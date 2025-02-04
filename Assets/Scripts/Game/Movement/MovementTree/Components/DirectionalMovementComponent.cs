using System.Collections.Generic;
using UnityEngine;

namespace Game.MovementTree
{
    /// <summary>
    /// A basic directional movement component.
    /// For example, if a piece can move up to 3 cells to the right,
    /// Direction is (1,0) and MaxLength is 3.
    /// This component will generate moves for each possible step.
    /// </summary>
    public class DirectionalMovementComponent : IMovementComponent
    {
        public Vector2Int Direction { get; private set; }
        public int MaxLength { get; private set; }

        public DirectionalMovementComponent(Vector2Int direction, int maxLength)
        {
            Direction = direction;
            MaxLength = maxLength;
        }

        public List<MovementNode> GenerateMoves(Vector2Int start)
        {
            List<MovementNode> nodes = new List<MovementNode>();
            for (int step = 1; step <= MaxLength; step++)
            {
                Vector2Int pos = start + Direction * step;
                nodes.Add(new MovementNode(pos, 0)); // MoveNumber will be set by the tree.
            }
            return nodes;
        }
    }
}