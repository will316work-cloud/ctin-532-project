using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] protected GameObject _summonedItemPrefab;
    [SerializeField] protected int _count;
    [SerializeField] protected Sprite _itemSprite;

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

    public virtual GameObject Summon(Vector2 position, Vector2 direction)
    {
        return Instantiate(_summonedItemPrefab, position, Quaternion.identity);//Quaternion.Euler(0, 0, _directionVectorToAngleDegreeFloat(direction)));
    }

    public void SetUpDisplay(ItemDisplay display)
    {
        display.SetSprite(_itemSprite);
        OnChangeCount = null;
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

    /*
    protected float _directionVectorToAngleDegreeFloat(Vector3 direction)
    {
        direction.Normalize();

        float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (n < 0)
        {
            n += 360;
        }

        return n;
    }
    */
}
