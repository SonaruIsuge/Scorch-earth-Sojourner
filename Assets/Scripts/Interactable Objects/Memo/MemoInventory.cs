using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Memo Inventory", menuName = "Memo System/Inventory")]
public class MemoInventory : ScriptableObject
{
    [SerializeField] private List<MemoData> AllMemoDataList;
    private Dictionary<int, MemoData> MemoDictionary;
    
    
    public void InitMemoDict()
    {
        MemoDictionary = new Dictionary<int, MemoData>();

        foreach (var memo in AllMemoDataList)
        {
            MemoDictionary.Add(memo.Id, memo);
        }
    }


    public MemoData GetMemoData(int id) => MemoDictionary.ContainsKey(id) ? MemoDictionary[id] : null;
}
