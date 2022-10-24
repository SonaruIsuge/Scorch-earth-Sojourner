
using SonaruUtilities;
using UnityEngine;

public sealed class ItemControlHandler : TSingletonMonoBehaviour<ItemControlHandler>
{
    [SerializeField] private RecordableInventory inventory;

    protected override void Awake()
    {
        base.Awake();
        
        inventory.InitItemDict();
    }


    public RecordableItem GetRecordableItemById(int id)
    {
        return inventory.ItemDictionary.ContainsKey(id) ? inventory.ItemDictionary[id] : null;
    }
}
