using Game;
using Game.Buffs;
using UnityEngine;

public class EnPassantBuff : IPieceBuff
{
    public int RemainingTurns { get; private set; }
    private Piece AppliedPiece { get; set; }
    private Vector2Int CapturePosition { get; set; }

    public EnPassantBuff(Vector2Int capturePosition)
    {
        RemainingTurns = 1;
        CapturePosition = capturePosition;
    }

    public void ApplyTo(Piece piece)
    {
        AppliedPiece = piece;
        OnApply();
    }

    public void Remove()
    {
        OnRemove();
        AppliedPiece = null;
    }

    public void DecrementDuration()
    {
        if (RemainingTurns > 0)
        {
            RemainingTurns--;
            if (IsExpired())
            {
                Remove();
            }
        }
    }

    public bool IsExpired() => RemainingTurns <= 0;

    public void OnApply()
    {
        // Implement En Passant logic
    }

    public void OnRemove()
    {
        // Clean up En Passant logic
    }

    public void OnMove(Vector2Int from, Vector2Int to)
    {
        // Handle En Passant move
    }

    public void OnAttack(Piece target)
    {
        // Handle En Passant attack if needed
    }

    public void OnCapture(Piece capturedPiece)
    {
        // Handle En Passant capture if needed
    }
}