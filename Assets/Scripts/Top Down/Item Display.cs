using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private Image _itemSprite;
    [SerializeField] private TMP_Text _itemCountText;

    [Header("Display Values")]
    [SerializeField] private Color _selectedShade;
    [SerializeField] private Vector3 _selectedScale;
    [SerializeField] private Color _notSelectedShade;
    [SerializeField] private Vector3 _notSelectedScale;

    private int _itemCount;

    private void Awake()
    {
        ToggleSelected(false);
        SetItemCount(0);
    }

    /*
    [ContextMenu("Increment Item Count")]
    public void IncrementItemCount()
    {
        SetItemCount(_itemCount + 1);
    }

    [ContextMenu("Decrement Item Count")]
    public void DecrementItemCount()
    {
        SetItemCount(_itemCount - 1);
    }
    */

    public void SetItemCount(int newCount)
    {
        _itemCount = newCount;

        if (_itemCount <= 0)
        {
            _itemSprite.gameObject.SetActive(false);
            _itemCountText.gameObject.SetActive(false);
        }
        else
        {
            _itemSprite.gameObject.SetActive(true);
            _itemCountText.gameObject.SetActive(true);
            _itemCountText.text = "" + _itemCount;
        }
    }

    /*
    [ContextMenu("Toggle On")]
    public void ToggleOn()
    {
        ToggleSelected(true);
    }

    [ContextMenu("Toggle Off")]
    public void ToggleOff()
    {
        ToggleSelected(false);
    }
    */

    public void ToggleSelected(bool isSelected)
    {
        if (isSelected)
        {
            SetShadeColor(_selectedShade);
            SetScale(_selectedScale);
        }
        else
        {
            SetShadeColor(_notSelectedShade);
            SetScale(_notSelectedScale);
        }
    }

    public void SetSprite(Sprite newSprite)
    {
        _itemSprite.sprite = newSprite;
    }

    private void SetShadeColor(Color newColor)
    {
        _itemSprite.color = newColor;
    }

    private void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}
