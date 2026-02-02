using UnityEngine;
using UnityEngine.Events;

public class UseItem : MonoBehaviour
{
    [SerializeField] private float _scrollScale = 0.2f;
    [SerializeField] private Transform _summonPosition;
    [SerializeField] private AudioSource _itemFailSound;
    [SerializeField] private Item[] _items;
    [SerializeField] private ItemDisplay _itemDisplayPrefab;
    [SerializeField] private Transform _displaySpawn;

    private float _selectedItemIndexFloat;
    private int _selectedItemIndex;
    private int _newIndex;
    private bool _willUseItem;

    private ItemDisplay[] _displays;

    private void Awake()
    {
        foreach (Item item in _items)
        {
            ItemDisplay display = Instantiate(_itemDisplayPrefab);
            item.SetUpDisplay(display);
            display.transform.SetParent(_displaySpawn);

            item.Count = 0;
        }

        _displays = _displaySpawn.GetComponentsInChildren<ItemDisplay>();
        _displays[_selectedItemIndex].ToggleSelected(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _willUseItem = true;
        }

        _selectedItemIndexFloat += Input.mouseScrollDelta.y * _scrollScale;
        _selectedItemIndexFloat = Mathf.Min(Mathf.Max(_selectedItemIndexFloat, 0), _items.Length - 1);
        _newIndex = (int)_selectedItemIndexFloat;
    }

    private void LateUpdate()
    {
        if (_selectedItemIndex != _newIndex)
        {
            _displays[_selectedItemIndex].ToggleSelected(false);
            _selectedItemIndex = _newIndex;
            _displays[_selectedItemIndex].ToggleSelected(true);
        }

        if (_willUseItem)
        {
            Item selectedItem = _items[_selectedItemIndex];

            if (selectedItem.Count > 0)
            {
                selectedItem.Summon(_summonPosition.position, _summonPosition.right);
                selectedItem.Count--;
            }
            else
            {
                _itemFailSound.Play();
            }

            _willUseItem = false;
        }
    }
}
