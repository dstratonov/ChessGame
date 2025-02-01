using System.Collections.Generic;
using System.Numerics;
using Unity.Collections;

namespace Game
{
    public interface IPieceMove
    {
        public List<(int,int)> GetMoves(Vector2 boardSize, int pieceX, int pieceY);
    }
}