using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform hinge;
    public float openAngle = 90f;
    public float openSpeed = 5f;
    public bool startOpen = false;

    bool isOpen;
    Quaternion closedRot;
    Quaternion openRot;

    void Start()
    {
        if (hinge == null) hinge = transform;
        closedRot = hinge.localRotation;
        openRot = closedRot * Quaternion.Euler(0f, openAngle, 0f);
        isOpen = startOpen;
        hinge.localRotation = isOpen ? openRot : closedRot;
    }

    public void ToggleDoor()
    {
        StopAllCoroutines();
        StartCoroutine(RotateDoor(isOpen ? closedRot : openRot));
        isOpen = !isOpen;
    }

    System.Collections.IEnumerator RotateDoor(Quaternion target)
    {
        float t = 0f;
        Quaternion start = hinge.localRotation;
        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            hinge.localRotation = Quaternion.Slerp(start, target, t);
            yield return null;
        }
        hinge.localRotation = target;
    }

    void OnMouseDown()
    {
        ToggleDoor();
    }
}
