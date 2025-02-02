using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Movement
{
    public class CompositeMovement : IMovementBehavior
    {
        public List<IMovementBehavior> Movements { get; private set; } = new List<IMovementBehavior>();

        public void Add(IMovementBehavior movement)
        {
            Movements.Add(movement);
        }

        public List<Vector2Int> GetAvailableMoves(Vector2Int currentPos, Board board, Piece piece)
        {
            // Use a HashSet to avoid duplicate positions.
            HashSet<Vector2Int> moves = new HashSet<Vector2Int>();
            foreach (var movement in Movements)
            {
                foreach (var move in movement.GetAvailableMoves(currentPos, board, piece))
                {
                    moves.Add(move);
                }
            }
            return moves.ToList();
        }
    }
}