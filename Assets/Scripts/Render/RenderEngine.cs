using System.Collections.Generic;
using UnityEngine;
using Game;
using Color = Game.Color;

public class RenderEngine : MonoBehaviour
{
    [Header("Cell Prefab & Materials")]
    // A single cell prefab.
    public GameObject cellPrefab;
    // Two materials to distinguish white and black cells.
    public Material whiteCellMaterial;
    public Material blackCellMaterial;

    [Header("Piece Prefabs (One per Type)")]
    public GameObject pawnPrefab;
    public GameObject rookPrefab;
    public GameObject knightPrefab;
    public GameObject bishopPrefab;
    public GameObject queenPrefab;
    public GameObject kingPrefab;

    [Header("Piece Materials")]
    public Material whiteMaterial;
    public Material blackMaterial;

    public float cellSize = 1f;

    // Dictionary mapping each piece type to its prefab.
    private Dictionary<PieceType, GameObject> piecePrefabs;

    // A slight vertical offset so pieces are rendered above the cell.
    private readonly Vector3 pieceYOffset = new Vector3(0f, 0.01f, 0f);
    // Default rotation for pieces.
    private readonly Quaternion pieceRotation = Quaternion.FromToRotation(Vector3.forward, Vector3.up);

    private void Awake()
    {
        // Initialize the dictionary with piece prefab references.
        piecePrefabs = new Dictionary<PieceType, GameObject>
        {
            { PieceType.Pawn, pawnPrefab },
            { PieceType.Rook, rookPrefab },
            { PieceType.Knight, knightPrefab },
            { PieceType.Bishop, bishopPrefab },
            { PieceType.Queen, queenPrefab },
            { PieceType.King, kingPrefab }
        };
    }

    /// <summary>
    /// Instantiates a cell and applies the correct material based on its board position.
    /// </summary>
    /// <param name="row">Row index of the cell.</param>
    /// <param name="col">Column index of the cell.</param>
    /// <returns>The instantiated cell GameObject.</returns>
    private GameObject GetCell(int row, int col)
    {
        GameObject cellObject = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity);
        CellController cellController = cellObject.GetComponent<CellController>();
        if(cellController != null)
        {
            cellController.Initialize(row, col);
        }
        
        // Choose the material based on the sum of the row and column.
        Material targetMaterial = ((row + col) % 2 == 1) ? whiteCellMaterial : blackCellMaterial;

        // Apply the material to all renderers (in case your prefab has children).
        Renderer[] renderers = cellObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = targetMaterial;
        }

        return cellObject;
    }

    /// <summary>
    /// Renders all cells and pieces on the board.
    /// </summary>
    /// <param name="cells">A dictionary of cells keyed by their (widthIndex, heightIndex).</param>
    public void RenderCells(Dictionary<(int, int), Cell> cells)
    {
        foreach (KeyValuePair<(int, int), Cell> entry in cells)
        {
            Cell cell = entry.Value;
            Vector3 worldPos = new Vector3(cell.WidthIndex * cellSize, 0f, cell.HeightIndex * cellSize);

            // Create and position the cell.
            GameObject cellObject = GetCell(cell.WidthIndex, cell.HeightIndex);
            cellObject.transform.position = worldPos;
            cellObject.transform.localScale = new Vector3(
                cellSize * cellObject.transform.lossyScale.x,
                0.05f,
                cellSize * cellObject.transform.lossyScale.z);

            // If there is a piece on this cell, instantiate it.
            if (cell.piece != null)
            {
                InstantiatePiece(cell.piece, worldPos);
            }
        }
    }

    /// <summary>
    /// Instantiates a chess piece and assigns the correct material.
    /// </summary>
    /// <param name="piece">The piece to instantiate.</param>
    /// <param name="basePosition">The base world position (center of the cell).</param>
    private void InstantiatePiece(Piece piece, Vector3 basePosition)
    {
        if (piecePrefabs.TryGetValue(piece.pieceType, out GameObject prefab))
        {
            GameObject pieceObject = Instantiate(prefab, basePosition + pieceYOffset, pieceRotation);
            // Choose the piece material based on its color.
            Material targetMaterial = piece.pieceColor == Color.Black ? blackMaterial : whiteMaterial;

            // Apply the material to all renderers within the piece.
            Renderer[] renderers = pieceObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.material = targetMaterial;
            }
        }
        else
        {
            Debug.LogWarning($"No prefab found for piece type: {piece.pieceType}");
        }
    }
}
