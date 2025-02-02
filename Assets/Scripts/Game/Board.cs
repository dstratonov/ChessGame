using System;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Game.Movement;
using Color = Game.Color;

public class Board : MonoBehaviour
{
    public RenderEngine renderEngine;
    
    // Board dimensions (should be at least 8 for standard chess setup)
    public int width = 8;
    public int height = 8;
    
    // Board cells stored in a dictionary keyed by (x, y)
    private Dictionary<(int, int), Cell> _cells = new Dictionary<(int, int), Cell>();

    void Start()
    {
        // Create the board and then initialize chess pieces in the center 8x8 region.
        InitializeBoard(width, height);
        InitPieces();
        RefreshBoard();
    }

    /// <summary>
    /// Creates a board with the specified dimensions.
    /// </summary>
    public void InitializeBoard(int newWidth, int newHeight)
    {
        width = newWidth;
        height = newHeight;
        _cells.Clear();
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _cells[(x, y)] = new Cell(x, y);
            }
        }
    }

    /// <summary>
    /// Re-renders the board using the current cell dictionary.
    /// </summary>
    public void RefreshBoard()
    {
        // Depending on your RenderEngine implementation, you might need to clear previous renderings.
        renderEngine.RenderCells(_cells);
    }
    
    public Cell GetCellAt(Vector2Int pos)
    {
        if (_cells.TryGetValue((pos.x, pos.y), out Cell cell))
            return cell;
        return null;
    }
    
    public bool IsWithinBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }
    

    /// <summary>
    /// Places standard chess pieces into the center 8x8 region of the board.
    /// For boards larger than 8x8, the extra rows/columns become margins.
    /// </summary>
    private void InitPieces()
{
    // Ensure the board is large enough for a standard chess setup.
    if (width < 8 || height < 8)
    {
        Debug.LogWarning("Board size is too small for a standard chess setup.");
        return;
    }

    // Compute margins so the standard 8x8 grid is centered.
    int marginX = (width - 8) / 2;
    int marginY = (height - 8) / 2;

    // For sliding pieces (rook, bishop, queen), use a maxSteps value that covers the board.
    int maxSteps = Math.Max(width, height);

    #region Pawns

    // White Pawns (placed on core row 1)
    for (int x = 0; x < 8; x++)
    {
        int posX = marginX + x;
        int posY = marginY + 1;
        if (_cells.ContainsKey((posX, posY)))
        {
            Piece pawn = new Piece(PieceType.Pawn, Color.White);
            // White pawns move upward.
            pawn.MovementBehavior = new DirectionalMovement(new Vector2Int(0, 1), 1);
            _cells[(posX, posY)].addPiece(pawn);
        }
    }

    // Black Pawns (placed on core row 6)
    for (int x = 0; x < 8; x++)
    {
        int posX = marginX + x;
        int posY = marginY + 6;
        if (_cells.ContainsKey((posX, posY)))
        {
            Piece pawn = new Piece(PieceType.Pawn, Color.Black);
            // Black pawns move downward.
            pawn.MovementBehavior = new DirectionalMovement(new Vector2Int(0, -1), 1);
            _cells[(posX, posY)].addPiece(pawn);
        }
    }

    #endregion

    #region White Major Pieces (Core Row 0)

    // White Rook (left)
    if (_cells.ContainsKey((marginX + 0, marginY + 0)))
    {
        Piece rook = new Piece(PieceType.Rook, Color.White);
        CompositeMovement rookMovement = new CompositeMovement();
        rookMovement.Add(new DirectionalMovement(new Vector2Int(0, 1), maxSteps));
        rookMovement.Add(new DirectionalMovement(new Vector2Int(0, -1), maxSteps));
        rookMovement.Add(new DirectionalMovement(new Vector2Int(1, 0), maxSteps));
        rookMovement.Add(new DirectionalMovement(new Vector2Int(-1, 0), maxSteps));
        rook.MovementBehavior = rookMovement;
        _cells[(marginX + 0, marginY + 0)].addPiece(rook);
    }

    // White Knight (left)
    if (_cells.ContainsKey((marginX + 1, marginY + 0)))
    {
        Piece knight = new Piece(PieceType.Knight, Color.White);
        knight.MovementBehavior = new KnightMovement();
        _cells[(marginX + 1, marginY + 0)].addPiece(knight);
    }

    // White Bishop (left)
    if (_cells.ContainsKey((marginX + 2, marginY + 0)))
    {
        Piece bishop = new Piece(PieceType.Bishop, Color.White);
        CompositeMovement bishopMovement = new CompositeMovement();
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(1, 1), maxSteps));
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(1, -1), maxSteps));
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(-1, 1), maxSteps));
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(-1, -1), maxSteps));
        bishop.MovementBehavior = bishopMovement;
        _cells[(marginX + 2, marginY + 0)].addPiece(bishop);
    }

    // White Queen
    if (_cells.ContainsKey((marginX + 3, marginY + 0)))
    {
        Piece queen = new Piece(PieceType.Queen, Color.White);
        CompositeMovement queenMovement = new CompositeMovement();
        // Rook-like moves:
        queenMovement.Add(new DirectionalMovement(new Vector2Int(0, 1), maxSteps));
        queenMovement.Add(new DirectionalMovement(new Vector2Int(0, -1), maxSteps));
        queenMovement.Add(new DirectionalMovement(new Vector2Int(1, 0), maxSteps));
        queenMovement.Add(new DirectionalMovement(new Vector2Int(-1, 0), maxSteps));
        // Bishop-like moves:
        queenMovement.Add(new DirectionalMovement(new Vector2Int(1, 1), maxSteps));
        queenMovement.Add(new DirectionalMovement(new Vector2Int(1, -1), maxSteps));
        queenMovement.Add(new DirectionalMovement(new Vector2Int(-1, 1), maxSteps));
        queenMovement.Add(new DirectionalMovement(new Vector2Int(-1, -1), maxSteps));
        queen.MovementBehavior = queenMovement;
        _cells[(marginX + 3, marginY + 0)].addPiece(queen);
    }

    // White King
    if (_cells.ContainsKey((marginX + 4, marginY + 0)))
    {
        Piece king = new Piece(PieceType.King, Color.White);
        CompositeMovement kingMovement = new CompositeMovement();
        // King moves only one step in any direction.
        kingMovement.Add(new DirectionalMovement(new Vector2Int(0, 1), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(0, -1), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(1, 0), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(-1, 0), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(1, 1), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(1, -1), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(-1, 1), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(-1, -1), 1));
        king.MovementBehavior = kingMovement;
        _cells[(marginX + 4, marginY + 0)].addPiece(king);
    }

    // White Bishop (right)
    if (_cells.ContainsKey((marginX + 5, marginY + 0)))
    {
        Piece bishop = new Piece(PieceType.Bishop, Color.White);
        CompositeMovement bishopMovement = new CompositeMovement();
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(1, 1), maxSteps));
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(1, -1), maxSteps));
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(-1, 1), maxSteps));
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(-1, -1), maxSteps));
        bishop.MovementBehavior = bishopMovement;
        _cells[(marginX + 5, marginY + 0)].addPiece(bishop);
    }

    // White Knight (right)
    if (_cells.ContainsKey((marginX + 6, marginY + 0)))
    {
        Piece knight = new Piece(PieceType.Knight, Color.White);
        knight.MovementBehavior = new KnightMovement();
        _cells[(marginX + 6, marginY + 0)].addPiece(knight);
    }

    // White Rook (right)
    if (_cells.ContainsKey((marginX + 7, marginY + 0)))
    {
        Piece rook = new Piece(PieceType.Rook, Color.White);
        CompositeMovement rookMovement = new CompositeMovement();
        rookMovement.Add(new DirectionalMovement(new Vector2Int(0, 1), maxSteps));
        rookMovement.Add(new DirectionalMovement(new Vector2Int(0, -1), maxSteps));
        rookMovement.Add(new DirectionalMovement(new Vector2Int(1, 0), maxSteps));
        rookMovement.Add(new DirectionalMovement(new Vector2Int(-1, 0), maxSteps));
        rook.MovementBehavior = rookMovement;
        _cells[(marginX + 7, marginY + 0)].addPiece(rook);
    }

    #endregion

    #region Black Major Pieces (Core Row 7)

    // Black Rook (left)
    if (_cells.ContainsKey((marginX + 0, marginY + 7)))
    {
        Piece rook = new Piece(PieceType.Rook, Color.Black);
        CompositeMovement rookMovement = new CompositeMovement();
        rookMovement.Add(new DirectionalMovement(new Vector2Int(0, 1), maxSteps));
        rookMovement.Add(new DirectionalMovement(new Vector2Int(0, -1), maxSteps));
        rookMovement.Add(new DirectionalMovement(new Vector2Int(1, 0), maxSteps));
        rookMovement.Add(new DirectionalMovement(new Vector2Int(-1, 0), maxSteps));
        rook.MovementBehavior = rookMovement;
        _cells[(marginX + 0, marginY + 7)].addPiece(rook);
    }

    // Black Knight (left)
    if (_cells.ContainsKey((marginX + 1, marginY + 7)))
    {
        Piece knight = new Piece(PieceType.Knight, Color.Black);
        knight.MovementBehavior = new KnightMovement();
        _cells[(marginX + 1, marginY + 7)].addPiece(knight);
    }

    // Black Bishop (left)
    if (_cells.ContainsKey((marginX + 2, marginY + 7)))
    {
        Piece bishop = new Piece(PieceType.Bishop, Color.Black);
        CompositeMovement bishopMovement = new CompositeMovement();
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(1, 1), maxSteps));
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(1, -1), maxSteps));
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(-1, 1), maxSteps));
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(-1, -1), maxSteps));
        bishop.MovementBehavior = bishopMovement;
        _cells[(marginX + 2, marginY + 7)].addPiece(bishop);
    }

    // Black Queen
    if (_cells.ContainsKey((marginX + 3, marginY + 7)))
    {
        Piece queen = new Piece(PieceType.Queen, Color.Black);
        CompositeMovement queenMovement = new CompositeMovement();
        // Rook-like moves:
        queenMovement.Add(new DirectionalMovement(new Vector2Int(0, 1), maxSteps));
        queenMovement.Add(new DirectionalMovement(new Vector2Int(0, -1), maxSteps));
        queenMovement.Add(new DirectionalMovement(new Vector2Int(1, 0), maxSteps));
        queenMovement.Add(new DirectionalMovement(new Vector2Int(-1, 0), maxSteps));
        // Bishop-like moves:
        queenMovement.Add(new DirectionalMovement(new Vector2Int(1, 1), maxSteps));
        queenMovement.Add(new DirectionalMovement(new Vector2Int(1, -1), maxSteps));
        queenMovement.Add(new DirectionalMovement(new Vector2Int(-1, 1), maxSteps));
        queenMovement.Add(new DirectionalMovement(new Vector2Int(-1, -1), maxSteps));
        queen.MovementBehavior = queenMovement;
        _cells[(marginX + 3, marginY + 7)].addPiece(queen);
    }

    // Black King
    if (_cells.ContainsKey((marginX + 4, marginY + 7)))
    {
        Piece king = new Piece(PieceType.King, Color.Black);
        CompositeMovement kingMovement = new CompositeMovement();
        kingMovement.Add(new DirectionalMovement(new Vector2Int(0, 1), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(0, -1), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(1, 0), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(-1, 0), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(1, 1), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(1, -1), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(-1, 1), 1));
        kingMovement.Add(new DirectionalMovement(new Vector2Int(-1, -1), 1));
        king.MovementBehavior = kingMovement;
        _cells[(marginX + 4, marginY + 7)].addPiece(king);
    }

    // Black Bishop (right)
    if (_cells.ContainsKey((marginX + 5, marginY + 7)))
    {
        Piece bishop = new Piece(PieceType.Bishop, Color.Black);
        CompositeMovement bishopMovement = new CompositeMovement();
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(1, 1), maxSteps));
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(1, -1), maxSteps));
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(-1, 1), maxSteps));
        bishopMovement.Add(new DirectionalMovement(new Vector2Int(-1, -1), maxSteps));
        bishop.MovementBehavior = bishopMovement;
        _cells[(marginX + 5, marginY + 7)].addPiece(bishop);
    }

    // Black Knight (right)
    if (_cells.ContainsKey((marginX + 6, marginY + 7)))
    {
        Piece knight = new Piece(PieceType.Knight, Color.Black);
        knight.MovementBehavior = new KnightMovement();
        _cells[(marginX + 6, marginY + 7)].addPiece(knight);
    }

    // Black Rook (right)
    if (_cells.ContainsKey((marginX + 7, marginY + 7)))
    {
        Piece rook = new Piece(PieceType.Rook, Color.Black);
        CompositeMovement rookMovement = new CompositeMovement();
        rookMovement.Add(new DirectionalMovement(new Vector2Int(0, 1), maxSteps));
        rookMovement.Add(new DirectionalMovement(new Vector2Int(0, -1), maxSteps));
        rookMovement.Add(new DirectionalMovement(new Vector2Int(1, 0), maxSteps));
        rookMovement.Add(new DirectionalMovement(new Vector2Int(-1, 0), maxSteps));
        rook.MovementBehavior = rookMovement;
        _cells[(marginX + 7, marginY + 7)].addPiece(rook);
    }

    #endregion
}

    void Update()
    {
        // For testing dynamic board resizing, you might trigger board updates here.
        // Example: if (Input.GetKeyDown(KeyCode.L)) { AddColumnsLeft(1); }
    }
}
