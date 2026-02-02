using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public UnityEvent OnOpenDoor;
    public UnityEvent OnCloseDoor;

    [SerializeField] private bool _isOpen;

    public void ToggleDoor(bool isOpen)
    {
        if (isOpen && !_isOpen)
        {
            OnOpenDoor?.Invoke();
        }
        else if (!isOpen && _isOpen)
        {
            OnCloseDoor?.Invoke();
        }

        _isOpen = isOpen;
    }
}
