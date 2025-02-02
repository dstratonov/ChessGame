using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{
    public interface IMovementBehavior
    {
        /// <summary>
        /// Returns a list of board positions (as Vector2Int) that the piece can move to,
        /// based on its current position, board context, and any internal parameters.
        /// </summary>
        List<Vector2Int> GetAvailableMoves(Vector2Int currentPos, Board board, Piece piece);
    }
}