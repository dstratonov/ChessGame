using System.Collections.Generic;
using UnityEngine;

namespace Game.MovementTree
{
    /// <summary>
    /// A node in the movement tree.
    /// LocalPosition is relative to the piece’s original location.
    /// MoveNumber indicates at which move/turn this position was reached.
    /// </summary>
    public class MovementNode
    {
        public Vector2Int LocalPosition { get; set; }
        public int MoveNumber { get; set; }
        public List<MovementNode> Children { get; private set; }

        public MovementNode(Vector2Int position, int moveNumber)
        {
            LocalPosition = position;
            MoveNumber = moveNumber;
            Children = new List<MovementNode>();
        }
    }
}