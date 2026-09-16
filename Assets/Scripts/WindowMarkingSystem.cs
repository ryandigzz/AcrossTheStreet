using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages marking and tagging of embassy windows.
/// Players can mark windows while zoomed in and add preset notes.
/// </summary>
public class WindowMarkingSystem : MonoBehaviour
{
    [System.Serializable]
    public class MarkedWindow
    {
        public int windowID;
        public string windowLabel;
        public List<string> tags = new List<string>();
    }

    private static WindowMarkingSystem instance;
    private Dictionary<int, MarkedWindow> markedWindows = new Dictionary<int, MarkedWindow>();

    private Camera playerCamera;
    private float rayCastDistance = 1000f;

    // Preset tags players can choose from
    public static readonly string[] PresetTags = new string[]
    {
        "Suspicious activity",
        "New arrival",
        "Meeting",
        "Lights on",
        "Curtains closed",
        "Person visible",
        "Vehicle nearby",
        "Unusual time",
        "Incomplete observation"
    };

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        playerCamera = Camera.main;
    }

    void Update()
    {
        // Press M to mark the window you're currently looking at (when zoomed)
        if (Input.GetKeyDown(KeyCode.M))
            MarkWindowUnderCrosshair();

        // Press Tab to open/close notebook
        if (Input.GetKeyDown(KeyCode.Tab))
            ToggleNotebook();
    }

    public void MarkWindowUnderCrosshair()
    {
        if (playerCamera == null) return;

        // Raycast from center of screen
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, rayCastDistance))
        {
            EmbassyWindow window = hit.collider.GetComponent<EmbassyWindow>();
            if (window != null)
            {
                OpenTagMenu(window);
            }
            else
            {
                Debug.Log("Not looking at a markable window. Attach EmbassyWindow script to the window object.");
            }
        }
    }

    void OpenTagMenu(EmbassyWindow window)
    {
        // For now, just add a default tag. You can expand this to open a UI menu.
        AddTag(window.WindowID, window.WindowLabel, "Observed");
        Debug.Log($"Marked window: {window.WindowLabel}");
    }

    public void AddTag(int windowID, string windowLabel, string tag)
    {
        if (!markedWindows.ContainsKey(windowID))
        {
            markedWindows[windowID] = new MarkedWindow
            {
                windowID = windowID,
                windowLabel = windowLabel
            };
        }

        if (!markedWindows[windowID].tags.Contains(tag))
        {
            markedWindows[windowID].tags.Add(tag);
        }
    }

    public void RemoveTag(int windowID, string tag)
    {
        if (markedWindows.ContainsKey(windowID))
            markedWindows[windowID].tags.Remove(tag);
    }

    public Dictionary<int, MarkedWindow> GetAllMarkedWindows()
    {
        return new Dictionary<int, MarkedWindow>(markedWindows);
    }

    public void ClearAllMarks()
    {
        markedWindows.Clear();
    }

    void ToggleNotebook()
    {
        // This will be called when Tab is pressed
        // We'll handle the actual UI separately
    }

    public static WindowMarkingSystem Instance => instance;
}
