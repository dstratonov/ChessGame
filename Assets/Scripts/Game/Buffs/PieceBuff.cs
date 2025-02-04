using UnityEngine;

namespace Game.Buffs
{
    public interface IPieceBuff
    {
        int RemainingTurns { get; }
        
        void ApplyTo(Piece piece);
        void Remove();
        void DecrementDuration();
        bool IsExpired();

        // Actions to be implemented by specific buff types
        void OnApply();
        void OnRemove();
        void OnMove(Vector2Int from, Vector2Int to);
        void OnAttack(Piece target);
        void OnCapture(Piece capturedPiece);
    }
}