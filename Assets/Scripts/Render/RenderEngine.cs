using System.Collections.Generic;
using Game;
using UnityEngine;
using Color = Game.Color;

public class RenderEngine : MonoBehaviour
{
    public GameObject whiteCell;
    public GameObject blackCell;
    
    public GameObject whitePawnPrefab;
    public GameObject blackPawnPrefab;
    
    public GameObject whiteRookPrefab;
    public GameObject blackRookPrefab;
    public GameObject whiteKnightPrefab;
    public GameObject blackKnightPrefab;
    public GameObject whiteBishopPrefab;
    public GameObject blackBishopPrefab;
    public GameObject whiteQueenPrefab;
    public GameObject blackQueenPrefab;
    public GameObject whiteKingPrefab;
    public GameObject blackKingPrefab;

    public float cellSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject GetCell(int row, int col)
    {
        return (row + col) % 2 == 0 ? Instantiate(whiteCell, Vector3.zero, Quaternion.identity) : Instantiate(blackCell, Vector3.zero, Quaternion.identity);
    }

    public void RenderCells(Dictionary<(int, int), Cell> cells)
    {
        foreach(KeyValuePair<(int, int), Cell> entry in cells)
        {
            Cell cell = entry.Value;
            Vector3 worldPos = new Vector3(cell.WidthIndex * cellSize, 0.0f, cell.HeightIndex * cellSize);
            GameObject cellObject = GetCell(cell.WidthIndex, cell.HeightIndex);
            
            cellObject.transform.position = worldPos;
            cellObject.transform.localScale = new Vector3(cellSize * cellObject.transform.lossyScale.x, 0.05f, cellSize * cellObject.transform.lossyScale.z);

            if (cell.piece != null)
            {
                switch (cell.piece.pieceType)
                {
                    case PieceType.Pawn:
                        if (cell.piece.pieceColor == Color.Black)
                        {
                            Instantiate(blackPawnPrefab, worldPos + new Vector3(0.0f, 0.01f, 0.0f), Quaternion.FromToRotation(Vector3.forward, Vector3.up));
                        }
                        else
                        {
                            Instantiate(whitePawnPrefab, worldPos + new Vector3(0.0f, 0.01f, 0.0f), Quaternion.FromToRotation(Vector3.forward, Vector3.up));
                        }
                        break;
                    case PieceType.Rook:
                        if (cell.piece.pieceColor == Color.Black)
                        {
                            Instantiate(blackRookPrefab, worldPos + new Vector3(0.0f, 0.01f, 0.0f), Quaternion.FromToRotation(Vector3.forward, Vector3.up));
                        }
                        else
                        {
                            Instantiate(whiteRookPrefab, worldPos + new Vector3(0.0f, 0.01f, 0.0f), Quaternion.FromToRotation(Vector3.forward, Vector3.up));
                        }
                        break;
                    case PieceType.Knight:
                        if (cell.piece.pieceColor == Color.Black)
                        {
                            Instantiate(blackKnightPrefab, worldPos + new Vector3(0.0f, 0.01f, 0.0f), Quaternion.FromToRotation(Vector3.forward, Vector3.up));
                        }
                        else
                        {
                            Instantiate(whiteKnightPrefab, worldPos + new Vector3(0.0f, 0.01f, 0.0f), Quaternion.FromToRotation(Vector3.forward, Vector3.up));
                        }
                        break;
                    case PieceType.Bishop:
                        if (cell.piece.pieceColor == Color.Black)
                        {
                            Instantiate(blackBishopPrefab, worldPos + new Vector3(0.0f, 0.01f, 0.0f), Quaternion.FromToRotation(Vector3.forward, Vector3.up));
                        }
                        else
                        {
                            Instantiate(whiteBishopPrefab, worldPos + new Vector3(0.0f, 0.01f, 0.0f), Quaternion.FromToRotation(Vector3.forward, Vector3.up));
                        }
                        break;
                    case PieceType.Queen:
                        if (cell.piece.pieceColor == Color.Black)
                        {
                            Instantiate(blackQueenPrefab, worldPos + new Vector3(0.0f, 0.01f, 0.0f), Quaternion.FromToRotation(Vector3.forward, Vector3.up));
                        }
                        else
                        {
                            Instantiate(whiteQueenPrefab, worldPos + new Vector3(0.0f, 0.01f, 0.0f), Quaternion.FromToRotation(Vector3.forward, Vector3.up));
                        }
                        break;
                    case PieceType.King:
                        if (cell.piece.pieceColor == Color.Black)
                        {
                            Instantiate(blackKingPrefab, worldPos + new Vector3(0.0f, 0.01f, 0.0f), Quaternion.FromToRotation(Vector3.forward, Vector3.up));
                        }
                        else
                        {
                            Instantiate(whiteKingPrefab, worldPos + new Vector3(0.0f, 0.01f, 0.0f), Quaternion.FromToRotation(Vector3.forward, Vector3.up));
                        }
                        break;
                }
            }
        }
    }
}
