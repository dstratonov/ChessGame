using System.Collections.Generic;
using UnityEngine;

namespace Game.MovementTree
{
    /// <summary>
    /// An example knight movement component.
    /// It generates moves according to standard L-shaped knight moves.
    /// </summary>
    public class KnightMovementComponent : IMovementComponent
    {
        private static readonly Vector2Int[] KnightOffsets = new Vector2Int[]
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

        public List<MovementNode> GenerateMoves(Vector2Int start)
        {
            List<MovementNode> nodes = new List<MovementNode>();
            foreach (Vector2Int offset in KnightOffsets)
            {
                nodes.Add(new MovementNode(start + offset, 0));
            }
            return nodes;
        }
    }
}