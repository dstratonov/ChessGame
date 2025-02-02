using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game;
using Game.Movement; // Contains the movement interfaces and implementations

public class PieceSelector : MonoBehaviour
{
    [Tooltip("Assign the Board object from the scene.")]
    public Board board;

    [Tooltip("Assign the main camera (or leave empty to use Camera.main).")]
    public Camera mainCamera;

    // Currently selected piece (if any)
    private Piece selectedPiece;

    // The board coordinate where the selected piece is located
    private Vector2Int selectedCellPosition;

    // Keep track of cells that have been highlighted so we can clear them later.
    private List<CellController> highlightedCells = new List<CellController>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera cam = mainCamera != null ? mainCamera : Camera.main;
            if (cam == null)
            {
                Debug.LogError("No camera available for raycasting.");
                return;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Try to get the CellController from the clicked object.
                CellController cellController = hit.collider.GetComponent<CellController>();
                if (cellController != null)
                {
                    // Use the stored board coordinates from the CellController.
                    Vector2Int pos = new Vector2Int(cellController.x, cellController.y);
                    Debug.Log($"Clicked cell at: {pos.x}, {pos.y}");

                    // Retrieve the logical cell from the board.
                    Cell cell = board.GetCellAt(pos);
                    if (cell != null)
                    {
                        if (cell.piece != null)
                        {
                            // Select the piece and highlight its valid moves.
                            selectedPiece = cell.piece;
                            selectedCellPosition = pos;
                            Debug.Log($"Selected piece: {selectedPiece.pieceType} ({selectedPiece.pieceColor}) at {pos}");
                            HighlightValidMoves(selectedPiece, pos);
                        }
                        else
                        {
                            // Optionally, if you click an empty cell, clear any highlights/selection.
                            ClearHighlights();
                            selectedPiece = null;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Highlights all cells that are valid moves for the given piece.
    /// </summary>
    /// <param name="piece">The piece whose valid moves will be highlighted.</param>
    /// <param name="currentPos">The board coordinate of the piece.</param>
    private void HighlightValidMoves(Piece piece, Vector2Int currentPos)
    {
        // Clear any previously highlighted cells.
        ClearHighlights();

        // Get the valid moves from the piece's movement behavior.
        List<Vector2Int> validMoves = piece.MovementBehavior.GetAvailableMoves(currentPos, board, piece);

        // For simplicity, find all cell controllers in the scene.
        // (For larger boards you might want to cache these references.)
        CellController[] allCellControllers = GameObject.FindObjectsOfType<CellController>();

        // Highlight the cells whose board coordinates are in the valid moves list.
        foreach (CellController cc in allCellControllers)
        {
            Vector2Int cellPos = new Vector2Int(cc.x, cc.y);
            if (validMoves.Contains(cellPos))
            {
                cc.SetHighlight(true);
                highlightedCells.Add(cc);
            }
        }
    }

    /// <summary>
    /// Clears any highlighted cells.
    /// </summary>
    private void ClearHighlights()
    {
        foreach (CellController cc in highlightedCells)
        {
            if (cc != null)
            {
                cc.SetHighlight(false);
            }
        }
        highlightedCells.Clear();
    }
}
