using System.Collections.Generic;
using UnityEngine;
using Game;
using Game.MovementTree; // For MovementTree and related components

public class PieceSelector : MonoBehaviour
{
    [Tooltip("Assign the Board object from the scene.")]
    public Board board;

    [Tooltip("Assign the RenderEngine object from the scene.")]
    public RenderEngine renderEngine;

    [Tooltip("Assign the main camera (or leave empty to use Camera.main).")]
    public Camera mainCamera;

    // Currently selected piece (if any)
    private Piece selectedPiece;

    // The board coordinate where the selected piece is located
    private Vector2Int selectedCellPosition;

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
                    Vector2Int pos = new Vector2Int(cellController.x, cellController.y);
                    Debug.Log($"Clicked cell at: {pos.x}, {pos.y}");

                    // Retrieve the logical cell from the board.
                    Cell cell = board.GetCellAt(pos);
                    if (cell != null)
                    {
                        if (cell.piece != null)
                        {
                            // A piece is present; select it and highlight its valid moves.
                            selectedPiece = cell.piece;
                            selectedCellPosition = pos;
                            Debug.Log($"Selected piece: {selectedPiece.pieceType} ({selectedPiece.pieceColor}) at {pos}");

                            // Use the piece's movement tree to get valid local moves.
                            if (selectedPiece.MovementTree != null)
                            {
                                // Get reachable positions relative to the piece's start (local offsets).
                                List<Vector2Int> localMoves = selectedPiece.MovementTree.GetAllReachablePositions();

                                // Convert local moves to global board positions by adding the piece's board position.
                                List<Vector2Int> validMoves = new List<Vector2Int>();
                                foreach (Vector2Int offset in localMoves)
                                {
                                    validMoves.Add(selectedCellPosition + offset);
                                }

                                // Let the renderer highlight these cells.
                                renderEngine.HighlightCells(validMoves);
                            }
                            else
                            {
                                // If no movement tree is assigned, clear any highlights.
                                renderEngine.ClearHighlights();
                            }
                        }
                        else
                        {
                            // Clicked on an empty cell: clear the selection and remove any highlights.
                            selectedPiece = null;
                            renderEngine.ClearHighlights();
                        }
                    }
                }
            }
        }
    }
}
