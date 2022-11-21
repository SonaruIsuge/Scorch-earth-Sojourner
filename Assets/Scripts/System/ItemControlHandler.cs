
using SonaruUtilities;
using UnityEngine;

public sealed class ItemControlHandler : TSingletonMonoBehaviour<ItemControlHandler>
{
    [SerializeField] private RecordableInventory recordableInventory;
    [SerializeField] private MemoInventory memoInventory;

    protected override void Awake()
    {
        base.Awake();
        
        recordableInventory.InitItemDict();
        memoInventory.InitMemoDict();
    }


    public RecordableItem GetRecordableItemById(int id)
    {
        return recordableInventory.ItemDictionary.ContainsKey(id) ? recordableInventory.ItemDictionary[id] : null;
    }


    public MemoData GetMemoData(int id) => memoInventory.GetMemoData(id);
}
