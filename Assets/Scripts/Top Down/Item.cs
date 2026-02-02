using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private GameObject _summonedItemPrefab;
    [SerializeField] private int _count;
    [SerializeField] private Sprite _itemSprite;

    public Action<int> OnChangeCount;

    public int Count
    {
        get => _count;
        set
        {
            _count = Mathf.Max(value, 0);
            OnChangeCount?.Invoke(_count);
        }
    }

    public virtual GameObject SummonItem(Vector2 position)
    {
        return Instantiate(_summonedItemPrefab, position, Quaternion.identity);
    }

    public void SetUpDisplay(ItemDisplay display)
    {
        display.SetSprite(_itemSprite);
        OnChangeCount += display.SetItemCount;
    }

    public void ChangeCount(int delta)
    {
        Count += delta;
    }

    [ContextMenu("Increment Item Count")]
    public void IncrementItemCount()
    {
        Count++;
    }

    [ContextMenu("Decrement Item Count")]
    public void DecrementItemCount()
    {
        Count--;
    }
}
