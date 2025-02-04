using System.Collections.Generic;
using UnityEngine;

namespace Game.MovementTree
{
    public class EnPassantMovementComponent : IMovementComponent
    {
        private Vector2Int _capturePosition;

        public EnPassantMovementComponent(Vector2Int capturePosition)
        {
            _capturePosition = capturePosition;
        }

        public List<MovementNode> GenerateMoves(Vector2Int start)
        {
            return new List<MovementNode>
            {
                new MovementNode(_capturePosition - start, 0)
            };
        }
    }
}