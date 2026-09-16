using UnityEngine;
using TMPro;

/// <summary>
/// Simple notebook UI that displays all marked windows and their tags.
/// </summary>
public class NotebookUI : MonoBehaviour
{
    [SerializeField] private GameObject notebookPanel;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;

    private bool isOpen = false;

    void Start()
    {
        if (notebookPanel == null)
        {
            Debug.LogWarning("NotebookUI: notebookPanel not assigned!");
            enabled = false;
            return;
        }
        
        notebookPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            ToggleNotebook();
    }

    public void ToggleNotebook()
    {
        isOpen = !isOpen;
        notebookPanel.SetActive(isOpen);

        if (isOpen)
        {
            RefreshNotebook();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void RefreshNotebook()
    {
        if (contentText == null) return;

        var markedWindows = WindowMarkingSystem.Instance.GetAllMarkedWindows();

        if (markedWindows.Count == 0)
        {
            contentText.text = "No observations yet.\n\nPress M while looking at a window to mark it.";
            return;
        }

        string content = "<b>FIELD NOTEBOOK</b>\n\n";
        foreach (var window in markedWindows.Values)
        {
            content += $"<u>{window.windowLabel}</u>\n";
            if (window.tags.Count == 0)
            {
                content += "  (No tags)\n";
            }
            else
            {
                foreach (var tag in window.tags)
                {
                    content += $"  • {tag}\n";
                }
            }
            content += "\n";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        }

        contentText.text = content;
    }

    public void Close()
    {
        isOpen = false;
        notebookPanel.SetActive(false);
    }
}
