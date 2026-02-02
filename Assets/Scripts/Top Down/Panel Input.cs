using UnityEngine;
using UnityEngine.Events;

public class PanelInput : MonoBehaviour
{
    public UnityEvent OnPullUpPanel;
    public UnityEvent OnPutDownPanel;

    private bool _panelIsOn;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePanel(!_panelIsOn);
        }
    }

    public void TogglePanel(bool panelIsUp)
    {
        if (panelIsUp)
        {
            OnPullUpPanel?.Invoke();
        }
        else
        {
            OnPutDownPanel?.Invoke();
        }

        _panelIsOn = panelIsUp;
    }
}
