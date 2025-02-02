using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{
    public class DirectionalMovement : IMovementBehavior
    {
        public Vector2Int Direction { get; set; }
        public int MaxSteps { get; set; }

        /// <summary>
        /// Optionally, you can add conditions (for example, only allow movement if the cell is empty).
        /// </summary>
        public DirectionalMovement(Vector2Int direction, int maxSteps)
        {
            Direction = direction;
            MaxSteps = maxSteps;
        }

        public List<Vector2Int> GetAvailableMoves(Vector2Int currentPos, Board board, Piece piece)
        {
            List<Vector2Int> moves = new List<Vector2Int>();

            // Move step-by-step until max steps or an obstacle is encountered.
            for (int step = 1; step <= MaxSteps; step++)
            {
                Vector2Int newPos = currentPos + Direction * step;
                if (!board.IsWithinBounds(newPos))
                    break;

                // (Optional) You can check for obstacles or enemy pieces here.
                // For example:
                // if (board.IsOccupied(newPos)) break;

                moves.Add(newPos);
            }

            return moves;
        }
    }
}