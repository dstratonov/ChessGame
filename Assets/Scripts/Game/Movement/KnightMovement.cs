using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{
    public class KnightMovement : IMovementBehavior
    {
        // Define all eight possible knight moves.
        private readonly Vector2Int[] knightMoves = new Vector2Int[]
        {
            new Vector2Int(2, 1),
            new Vector2Int(2, -1),
            new Vector2Int(-2, 1),
            new Vector2Int(-2, -1),
            new Vector2Int(1, 2),
            new Vector2Int(1, -2),
            new Vector2Int(-1, 2),
            new Vector2Int(-1, -2)
        };

        /// <summary>
        /// Calculates all valid moves for a knight from the current position.
        /// </summary>
        /// <param name="currentPos">The current board position of the knight.</param>
        /// <param name="board">The board instance (used for bounds checking).</param>
        /// <param name="piece">The piece for which the move is being calculated (not used in this basic implementation).</param>
        /// <returns>A list of positions (as Vector2Int) that the knight can move to.</returns>
        public List<Vector2Int> GetAvailableMoves(Vector2Int currentPos, Board board, Piece piece)
        {
            List<Vector2Int> moves = new List<Vector2Int>();

            foreach (Vector2Int move in knightMoves)
            {
                Vector2Int newPos = currentPos + move;
                if (board.IsWithinBounds(newPos))
                {
                    moves.Add(newPos);
                }
            }

            return moves;
        }
    }
}