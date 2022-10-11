using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "Recordable/Inventory", fileName = "Recordable Item Inventory")]
public class RecordableInventory : ScriptableObject
{
    public List<RecordableItem> RecordableItems;
    public Dictionary<int, RecordableItem> ItemDictionary;


    public void InitItemDict()
    {
        ItemDictionary = new Dictionary<int, RecordableItem>();

        foreach (var item in RecordableItems)
        {
            ItemDictionary.Add(item.ItemId, item);
        }
    }


    public void AddItem(RecordableItem newItem)
    {
        if(ItemDictionary.ContainsKey(newItem.ItemId)) return;
        
        if (ItemDictionary.Count <= 0) ItemDictionary = new Dictionary<int, RecordableItem>();
        ItemDictionary.Add(newItem.ItemId, newItem);
    }


    public void RemoveItem(RecordableItem item)
    {
        if (ItemDictionary.ContainsKey(item.ItemId))
        {
            ItemDictionary.Remove(item.ItemId);
        }
    }
}
