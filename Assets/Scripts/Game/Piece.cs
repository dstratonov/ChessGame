using System.Collections.Generic;
using UnityEngine;
using Game.MovementTree;

namespace Game
{
    public enum PieceType
    {
        Pawn,
        Rook,
        Bishop,
        Knight,
        Queen,
        King,
        Empty
    }

    public enum Color
    {
        Black,
        White,
        None
    }

    public class Piece
    {
        public PieceType pieceType;
        public Color pieceColor;

        // Fields for piece position
        public int x { get; private set; }
        public int y { get; private set; }

        // MovementTree to handle all movement logic
        public MovementTree.MovementTree MovementTree { get; private set; }

        public Piece(PieceType pieceType, Color pieceColor, int x, int y)
        {
            this.pieceType = pieceType;
            this.pieceColor = pieceColor;
            this.x = x;
            this.y = y;
            MovementTree = new MovementTree.MovementTree();
        }

        // Method to update the piece's position
        public void SetPosition(int newX, int newY)
        {
            x = newX;
            y = newY;
        }

        // Method to get the piece's position as a Vector2Int
        public Vector2Int GetPosition()
        {
            return new Vector2Int(x, y);
        }

        // Method to get all reachable positions
        public List<Vector2Int> GetReachablePositions()
        {
            return MovementTree.GetAllReachablePositionsWithExtras();
        }

        // Method to add a movement component
        public void AddMovementComponent(IMovementComponent component, int targetMove)
        {
            MovementTree.AddComponent(component, targetMove);
        }

        // Method to add extra movement components
        public void AddExtraMovement(IMovementComponent component, int targetMove)
        {
            MovementTree.ExtraMoveComponents.Add(new ExtraMovementEntry(targetMove, component));
        }

        // Method to clear extra movement components
        public void ClearExtraMovements()
        {
            MovementTree.ExtraMoveComponents.Clear();
        }
    }
}