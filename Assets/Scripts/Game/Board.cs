using System;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Game.MovementTree;
using Color = Game.Color;

public class Board : MonoBehaviour
{
    public RenderEngine renderEngine;
    
    public int Width { get; private set; } = 8;
    public int Height { get; private set; } = 8;
    
    private Dictionary<(int, int), Cell> _cells = new Dictionary<(int, int), Cell>();

    void Start()
    {
        InitializeBoard(Width, Height);
        InitializePieces();
        RefreshBoard();
    }

    public void InitializeBoard(int width, int height)
    {
        Width = width;
        Height = height;
        _cells.Clear();
        
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _cells[(x, y)] = new Cell(x, y);
            }
        }
    }

    public void RefreshBoard()
    {
        renderEngine.RenderCells(_cells);
    }
    
    public Cell GetCellAt(Vector2Int pos)
    {
        return _cells.TryGetValue((pos.x, pos.y), out Cell cell) ? cell : null;
    }
    
    public bool IsWithinBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < Width && pos.y >= 0 && pos.y < Height;
    }

    private void InitializePieces()
    {
        if (Width < 8 || Height < 8)
        {
            Debug.LogWarning("Board size is too small for a standard chess setup.");
            return;
        }

        int marginX = (Width - 8) / 2;
        int marginY = (Height - 8) / 2;
        int maxSteps = Math.Max(Width, Height);

        InitializePawns(marginX, marginY);
        InitializeMajorPieces(marginX, marginY, maxSteps);
    }

    private void InitializePawns(int marginX, int marginY)
    {
        for (int x = 0; x < 8; x++)
        {
            CreatePawn(Color.White, new Vector2Int(marginX + x, marginY + 1), new Vector2Int(0, 1));
            CreatePawn(Color.Black, new Vector2Int(marginX + x, marginY + 6), new Vector2Int(0, -1));
        }
    }

    private void CreatePawn(Color color, Vector2Int position, Vector2Int direction)
    {
        Piece pawn = new Piece(PieceType.Pawn, color, position.x, position.y);
        pawn.AddMovementComponent(new DirectionalMovementComponent(direction, 2), 0);
        _cells[(position.x, position.y)].addPiece(pawn);
    }

    private void InitializeMajorPieces(int marginX, int marginY, int maxSteps)
    {
        InitializeRooks(marginX, marginY, maxSteps);
        InitializeKnights(marginX, marginY);
        InitializeBishops(marginX, marginY, maxSteps);
        InitializeQueens(marginX, marginY, maxSteps);
        InitializeKings(marginX, marginY);
    }

    private void InitializeRooks(int marginX, int marginY, int maxSteps)
    {
        CreateRook(Color.White, new Vector2Int(marginX, marginY), maxSteps);
        CreateRook(Color.White, new Vector2Int(marginX + 7, marginY), maxSteps);
        CreateRook(Color.Black, new Vector2Int(marginX, marginY + 7), maxSteps);
        CreateRook(Color.Black, new Vector2Int(marginX + 7, marginY + 7), maxSteps);
    }

    private void CreateRook(Color color, Vector2Int position, int maxSteps)
    {
        Piece rook = new Piece(PieceType.Rook, color, position.x, position.y);
        AddOrthogonalMovements(rook, maxSteps);
        _cells[(position.x, position.y)].addPiece(rook);
    }

    private void InitializeKnights(int marginX, int marginY)
    {
        CreateKnight(Color.White, new Vector2Int(marginX + 1, marginY));
        CreateKnight(Color.White, new Vector2Int(marginX + 6, marginY));
        CreateKnight(Color.Black, new Vector2Int(marginX + 1, marginY + 7));
        CreateKnight(Color.Black, new Vector2Int(marginX + 6, marginY + 7));
    }

    private void CreateKnight(Color color, Vector2Int position)
    {
        Piece knight = new Piece(PieceType.Knight, color, position.x, position.y);
        knight.AddMovementComponent(new KnightMovementComponent(), 0);
        _cells[(position.x, position.y)].addPiece(knight);
    }

    private void InitializeBishops(int marginX, int marginY, int maxSteps)
    {
        CreateBishop(Color.White, new Vector2Int(marginX + 2, marginY), maxSteps);
        CreateBishop(Color.White, new Vector2Int(marginX + 5, marginY), maxSteps);
        CreateBishop(Color.Black, new Vector2Int(marginX + 2, marginY + 7), maxSteps);
        CreateBishop(Color.Black, new Vector2Int(marginX + 5, marginY + 7), maxSteps);
    }

    private void CreateBishop(Color color, Vector2Int position, int maxSteps)
    {
        Piece bishop = new Piece(PieceType.Bishop, color, position.x, position.y);
        AddDiagonalMovements(bishop, maxSteps);
        _cells[(position.x, position.y)].addPiece(bishop);
    }

    private void InitializeQueens(int marginX, int marginY, int maxSteps)
    {
        CreateQueen(Color.White, new Vector2Int(marginX + 3, marginY), maxSteps);
        CreateQueen(Color.Black, new Vector2Int(marginX + 3, marginY + 7), maxSteps);
    }

    private void CreateQueen(Color color, Vector2Int position, int maxSteps)
    {
        Piece queen = new Piece(PieceType.Queen, color, position.x, position.y);
        AddOrthogonalMovements(queen, maxSteps);
        AddDiagonalMovements(queen, maxSteps);
        _cells[(position.x, position.y)].addPiece(queen);
    }

    private void InitializeKings(int marginX, int marginY)
    {
        CreateKing(Color.White, new Vector2Int(marginX + 4, marginY));
        CreateKing(Color.Black, new Vector2Int(marginX + 4, marginY + 7));
    }

    private void CreateKing(Color color, Vector2Int position)
    {
        Piece king = new Piece(PieceType.King, color, position.x, position.y);
        AddOrthogonalMovements(king, 1);
        AddDiagonalMovements(king, 1);
        _cells[(position.x, position.y)].addPiece(king);
    }

    private void AddOrthogonalMovements(Piece piece, int maxSteps)
    {
        piece.AddMovementComponent(new DirectionalMovementComponent(Vector2Int.up, maxSteps), 0);
        piece.AddMovementComponent(new DirectionalMovementComponent(Vector2Int.down, maxSteps), 0);
        piece.AddMovementComponent(new DirectionalMovementComponent(Vector2Int.left, maxSteps), 0);
        piece.AddMovementComponent(new DirectionalMovementComponent(Vector2Int.right, maxSteps), 0);
    }

    private void AddDiagonalMovements(Piece piece, int maxSteps)
    {
        piece.AddMovementComponent(new DirectionalMovementComponent(new Vector2Int(1, 1), maxSteps), 0);
        piece.AddMovementComponent(new DirectionalMovementComponent(new Vector2Int(1, -1), maxSteps), 0);
        piece.AddMovementComponent(new DirectionalMovementComponent(new Vector2Int(-1, 1), maxSteps), 0);
        piece.AddMovementComponent(new DirectionalMovementComponent(new Vector2Int(-1, -1), maxSteps), 0);
    }
}