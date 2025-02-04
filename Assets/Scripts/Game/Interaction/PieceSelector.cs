using System.Collections.Generic;
using UnityEngine;
using Game;
using Game.Movement;

public class PieceSelector : MonoBehaviour
{
    [Tooltip("Assign the Board object from the scene.")]
    public Board board;

    [Tooltip("Assign the RenderEngine object from the scene.")]
    public RenderEngine renderEngine;

    [Tooltip("Assign the main camera (or leave empty to use Camera.main).")]
    public Camera mainCamera;

    private Piece selectedPiece;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
    }

    private void HandleMouseClick()
    {
        Vector2Int? clickedPosition = GetClickedCellPosition();
        if (!clickedPosition.HasValue) return;

        Vector2Int pos = clickedPosition.Value;
        Cell cell = board.GetCellAt(pos);
        if (cell == null) return;

        if (cell.piece != null)
        {
            SelectPiece(cell.piece);
        }
        else
        {
            ClearSelection();
        }
    }

    private Vector2Int? GetClickedCellPosition()
    {
        Camera cam = mainCamera != null ? mainCamera : Camera.main;
        if (cam == null)
        {
            Debug.LogError("No camera available for raycasting.");
            return null;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            CellController cellController = hit.collider.GetComponent<CellController>();
            if (cellController != null)
            {
                return new Vector2Int(cellController.x, cellController.y);
            }
        }

        return null;
    }

    private void SelectPiece(Piece piece)
    {
        selectedPiece = piece;
        Vector2Int piecePosition = piece.GetPosition();
        Debug.Log($"Selected piece: {selectedPiece.pieceType} ({selectedPiece.pieceColor}) at {piecePosition}");

        List<Vector2Int> validMoves = MovementValidator.GetValidMoves(board, piece);
        renderEngine.HighlightCells(validMoves);
    }

    private void ClearSelection()
    {
        selectedPiece = null;
        renderEngine.ClearHighlights();
    }
}