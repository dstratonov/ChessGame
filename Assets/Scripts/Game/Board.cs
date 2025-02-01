using System.Collections.Generic;
using Game;
using UnityEngine;
using Color = Game.Color;

public class Board : MonoBehaviour
{
    public RenderEngine renderEngine;
    
    public int width;
    public int height;
    
    private Dictionary<(int, int), Cell> _cells = new Dictionary<(int, int), Cell>();
    void Start()
    {
        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
        {
            _cells[(x, y)] = new Cell(x, y);
        }
        
        
        InitPieces();

        renderEngine.RenderCells(_cells);
    }

    private void InitPieces()
    {
        Piece whitePawn = new Piece(PieceType.Pawn, Color.White);
        _cells[(0, 1)].addPiece(whitePawn);
        _cells[(1, 1)].addPiece(whitePawn);
        _cells[(2, 1)].addPiece(whitePawn);
        _cells[(3, 1)].addPiece(whitePawn);
        _cells[(4, 1)].addPiece(whitePawn);
        _cells[(5, 1)].addPiece(whitePawn);
        _cells[(6, 1)].addPiece(whitePawn);
        _cells[(7, 1)].addPiece(whitePawn);
        
        Piece blackPawn = new Piece(PieceType.Pawn, Color.Black);
        
        _cells[(0, 6)].addPiece(blackPawn);
        _cells[(1, 6)].addPiece(blackPawn);
        _cells[(2, 6)].addPiece(blackPawn);
        _cells[(3, 6)].addPiece(blackPawn);
        _cells[(4, 6)].addPiece(blackPawn);
        _cells[(5, 6)].addPiece(blackPawn);
        _cells[(6, 6)].addPiece(blackPawn);
        _cells[(7, 6)].addPiece(blackPawn);
        
        Piece whiteRook = new Piece(PieceType.Rook, Color.White);
        _cells[(0, 0)].addPiece(whiteRook);
        _cells[(7, 0)].addPiece(whiteRook);
        
        Piece blackRook = new Piece(PieceType.Rook, Color.Black);
        _cells[(0, 7)].addPiece(blackRook);
        _cells[(7, 7)].addPiece(blackRook);
        
        Piece whiteKnight = new Piece(PieceType.Knight, Color.White);
        _cells[(1, 0)].addPiece(whiteKnight);
        _cells[(6, 0)].addPiece(whiteKnight);
        
        Piece blackKnight = new Piece(PieceType.Knight, Color.Black);
        _cells[(1, 7)].addPiece(blackKnight);
        _cells[(6, 7)].addPiece(blackKnight);
        
        Piece whiteBishop = new Piece(PieceType.Bishop, Color.White);
        _cells[(2, 0)].addPiece(whiteBishop);
        _cells[(5, 0)].addPiece(whiteBishop);
        
        Piece blackBishop = new Piece(PieceType.Bishop, Color.Black);
        _cells[(2, 7)].addPiece(blackBishop);
        _cells[(5, 7)].addPiece(blackBishop);
        
        Piece whiteQueen = new Piece(PieceType.Queen, Color.White);
        _cells[(3, 0)].addPiece(whiteQueen);
        
        Piece blackQueen = new Piece(PieceType.Queen, Color.Black);
        _cells[(3, 7)].addPiece(blackQueen);
        
        Piece whiteKing = new Piece(PieceType.King, Color.White);
        _cells[(4, 0)].addPiece(whiteQueen);
        
        Piece blackKing = new Piece(PieceType.King, Color.Black);
        _cells[(4, 7)].addPiece(blackKing);
        
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
