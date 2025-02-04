using System.Collections.Generic;
using UnityEngine;

namespace Game.MovementTree
{
    /// <summary>
    /// Wraps a movement building block.
    /// A movement component is something that, given a starting (local) position,
    /// generates a list of moves (as MovementNodes).
    /// </summary>
    public interface IMovementComponent
    {
        List<MovementNode> GenerateMoves(Vector2Int start);
    }
}