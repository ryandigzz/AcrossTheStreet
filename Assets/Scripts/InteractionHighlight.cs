using UnityEngine;

/// <summary>
/// Detects interactable objects in the center of the screen and highlights them with an outline.
/// </summary>
public class InteractionHighlight : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float raycastDistance = 100f;
    [SerializeField] private LayerMask interactableLayers = ~0;
    
    [Header("Outline Settings")]
    [SerializeField] private Color outlineColor = Color.yellow;
    [SerializeField] private float outlineWidth = 3f;

    private Camera playerCamera;
    private GameObject currentHighlighted;
    private Outline currentOutline;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        CheckForInteractable();
    }

    void CheckForInteractable()
    {
        if (playerCamera == null) return;

        // Raycast from center of screen
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, interactableLayers))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if this object is interactable
            if (IsInteractable(hitObject))
            {
                // If we're already highlighting this object, do nothing
                if (currentHighlighted == hitObject)
                    return;

                // Clear previous highlight
                ClearHighlight();

                // Highlight new object
                HighlightObject(hitObject);
            }
            else
            {
                ClearHighlight();
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    bool IsInteractable(GameObject obj)
    {
        // Check if the object or its parents have interactable components
        return obj.GetComponent<EmbassyWindow>() != null ||
               obj.GetComponentInParent<EmbassyWindow>() != null ||
               obj.GetComponent<DoorController>() != null ||
               obj.GetComponentInParent<DoorController>() != null ||
               obj.GetComponent<Unity.Cinemachine.CinemachineCamera>() != null ||
               obj.GetComponentInParent<Unity.Cinemachine.CinemachineCamera>() != null;
    }

    void HighlightObject(GameObject obj)
    {
        currentHighlighted = obj;

        // Get or add Outline component
        currentOutline = obj.GetComponent<Outline>();
        if (currentOutline == null)
        {
            currentOutline = obj.AddComponent<Outline>();
        }

        // Configure outline
        currentOutline.OutlineMode = Outline.Mode.OutlineVisible;
        currentOutline.OutlineColor = outlineColor;
        currentOutline.OutlineWidth = outlineWidth;
        currentOutline.enabled = true;
    }

    void ClearHighlight()
    {
        if (currentHighlighted != null && currentOutline != null)
        {
            currentOutline.enabled = false;
            currentHighlighted = null;
            currentOutline = null;
        }
    }

    void OnDisable()
    {
        ClearHighlight();
    }
}
