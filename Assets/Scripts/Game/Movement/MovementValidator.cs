using System.Collections.Generic;
using UnityEngine;
using Game.MovementTree;

namespace Game.Movement
{
    public static class MovementValidator
    {
        public static List<Vector2Int> GetValidMoves(Board board, Piece piece)
        {
            List<Vector2Int> validMoves = new List<Vector2Int>();
            if (piece.MovementTree?.Root == null) return validMoves;

            Vector2Int startPosition = piece.GetPosition();
            ValidateMovesRecursive(board, piece, piece.MovementTree.Root, startPosition, validMoves);
            return validMoves;
        }

        private static void ValidateMovesRecursive(Board board, Piece piece, MovementNode node, Vector2Int startPosition, List<Vector2Int> validMoves)
        {
            // Skip the root node (current position)
            if (node != piece.MovementTree.Root)
            {
                Vector2Int globalPosition = startPosition + node.LocalPosition;

                // Check board bounds
                if (!board.IsWithinBounds(globalPosition))
                    return; // Stop exploring this branch

                Cell targetCell = board.GetCellAt(globalPosition);
                
                // If cell is occupied
                if (targetCell.piece != null)
                {
                    // If it's an enemy piece, add as valid move (capture) but don't continue path
                    if (targetCell.piece.pieceColor != piece.pieceColor)
                    {
                        validMoves.Add(node.LocalPosition);
                    }
                    return; // Stop exploring this branch
                }
                
                // Empty cell - add to valid moves
                validMoves.Add(node.LocalPosition);
            }

            // Continue DFS only if path isn't blocked
            foreach (MovementNode child in node.Children)
            {
                ValidateMovesRecursive(board, piece, child, startPosition, validMoves);
            }
        }
    }
}