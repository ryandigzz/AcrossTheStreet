using UnityEngine;

/// <summary>
/// Identifies and marks an embassy window for the marking system.
/// Attach this to each window GameObject you want to be markable.
/// </summary>
public class EmbassyWindow : MonoBehaviour
{
    [SerializeField] private int windowID = 0;
    [SerializeField] private string windowLabel = "Window"; // e.g., "3B", "Room 2", "Corner Office"

    public int WindowID => windowID;
    public string WindowLabel => windowLabel;

    private void OnValidate()
    {
        // Auto-generate ID from sibling order if not set
        if (windowID == 0)
            windowID = transform.GetSiblingIndex();
    }
}
