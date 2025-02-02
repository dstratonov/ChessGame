using UnityEngine;

public class CellController : MonoBehaviour
{
    // Board coordinates for this cell.
    public int x;
    public int y;

    // References to indicator GameObjects to show highlight and attacked states.
    [SerializeField] private GameObject highlightIndicator;
    [SerializeField] private GameObject attackedIndicator;

    /// <summary>
    /// Initializes the cell's board coordinates.
    /// </summary>
    /// <param name="x">The x index (column).</param>
    /// <param name="y">The y index (row).</param>
    public void Initialize(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Activates or deactivates the highlight indicator.
    /// </summary>
    /// <param name="enabled">True to enable highlighting; false to disable.</param>
    public void SetHighlight(bool enabled)
    {
        if (highlightIndicator != null)
        {
            highlightIndicator.SetActive(enabled);
        }
    }

    /// <summary>
    /// Activates or deactivates the attacked indicator.
    /// </summary>
    /// <param name="enabled">True to show the attacked indicator; false to hide it.</param>
    public void SetAttacked(bool enabled)
    {
        if (attackedIndicator != null)
        {
            attackedIndicator.SetActive(enabled);
        }
    }

    /// <summary>
    /// Resets both indicators by turning them off.
    /// </summary>
    public void ResetIndicators()
    {
        SetHighlight(false);
        SetAttacked(false);
    }
}