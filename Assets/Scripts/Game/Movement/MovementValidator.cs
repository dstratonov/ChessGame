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

            // Apply extra moves
            List<MovementNode> leafNodes = new List<MovementNode>();
            GetLeafNodes(piece.MovementTree.Root, leafNodes);

            foreach (MovementNode leaf in leafNodes)
            {
                foreach (ExtraMovementEntry extra in piece.MovementTree.ExtraMoveComponents)
                {
                    if (leaf.MoveNumber == extra.TargetMove)
                    {
                        List<MovementNode> extraNodes = extra.Component.GenerateMoves(leaf.LocalPosition);
                        foreach (MovementNode extraNode in extraNodes)
                        {
                            Vector2Int globalPosition = startPosition + extraNode.LocalPosition;
                            if (IsValidMove(board, piece, globalPosition))
                            {
                                validMoves.Add(globalPosition);
                            }
                        }
                    }
                }
            }

            return validMoves;
        }

        private static void ValidateMovesRecursive(Board board, Piece piece, MovementNode node, Vector2Int startPosition, List<Vector2Int> validMoves)
        {
            if (node != piece.MovementTree.Root)
            {
                Vector2Int globalPosition = startPosition + node.LocalPosition;

                if (IsValidMove(board, piece, globalPosition))
                {
                    validMoves.Add(globalPosition);

                    // For sliding pieces, continue exploring the path only if the cell is empty
                    if ((piece.pieceType == PieceType.Rook || piece.pieceType == PieceType.Bishop || piece.pieceType == PieceType.Queen) 
                        && board.GetCellAt(globalPosition).piece == null)
                    {
                        foreach (MovementNode child in node.Children)
                        {
                            ValidateMovesRecursive(board, piece, child, startPosition, validMoves);
                        }
                    }
                }
                else
                {
                    // Stop exploring this path for sliding pieces
                    return;
                }
            }

            // For non-sliding pieces or the root node, explore all children
            if (piece.pieceType != PieceType.Rook && piece.pieceType != PieceType.Bishop && piece.pieceType != PieceType.Queen)
            {
                foreach (MovementNode child in node.Children)
                {
                    ValidateMovesRecursive(board, piece, child, startPosition, validMoves);
                }
            }
        }

        private static bool IsValidMove(Board board, Piece piece, Vector2Int globalPosition)
        {
            if (!board.IsWithinBounds(globalPosition))
                return false;

            Cell targetCell = board.GetCellAt(globalPosition);
            
            if (targetCell.piece == null)
                return true;

            return targetCell.piece.pieceColor != piece.pieceColor;
        }

        private static void GetLeafNodes(MovementNode node, List<MovementNode> leaves)
        {
            if (node.Children.Count == 0)
            {
                leaves.Add(node);
            }
            else
            {
                foreach (MovementNode child in node.Children)
                {
                    GetLeafNodes(child, leaves);
                }
            }
        }
    }
}