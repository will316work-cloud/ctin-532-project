using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ActionsOnExposureType
{
    [SerializeField] private ExposureType _exposureType;
    public UnityEvent OnSeeExposureType;
    public UnityEvent OnNotSeeExposureType;

    private bool _isSeen;

    public void DoActionsOnExposed(bool isSeen, ExposureType type)
    {
        if (_exposureType != type)
        {
            return;
        }

        if (isSeen && !_isSeen)
        {
            OnSeeExposureType?.Invoke();
        }
        else if (!isSeen && _isSeen)
        {
            OnNotSeeExposureType?.Invoke();
        }

        _isSeen = isSeen;
    }

    public bool ActOnSeeExposureType(ExposureType type)
    {
        if (_exposureType == type)
        {
            OnSeeExposureType?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ActOnNotSeeExposureType(ExposureType type)
    {
        if (_exposureType == type)
        {
            OnNotSeeExposureType?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }
}
