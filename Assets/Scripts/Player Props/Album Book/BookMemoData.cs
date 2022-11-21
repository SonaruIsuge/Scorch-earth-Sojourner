using System.Collections.Generic;
using UnityEngine;


public class BookMemoData : MonoBehaviour
{
    [field: SerializeField] public List<int> AllMemoId { get; private set; }
    private Dictionary<int, MemoData> memoDataDict;


    public void LoadAllMemo()
    {
        AllMemoId = new List<int>();
        memoDataDict = new Dictionary<int, MemoData>();

        AllMemoId = GameDataHandler.Instance.SaveData.AllGetMemoId;
        UpdateDictionary();
    }


    private void UpdateDictionary()
    {
        foreach (var memo in AllMemoId) memoDataDict.Add(memo, ItemControlHandler.Instance.GetMemoData(memo));
    }
    
    
    public void UpdateData()
    {
        AllMemoId = GameDataHandler.Instance.SaveData.AllGetMemoId;
        memoDataDict.Clear();
        UpdateDictionary();
    }


    public void AddNewMemo(MemoData data)
    {
        if(memoDataDict.ContainsKey(data.Id)) return;
        
        memoDataDict.Add(data.Id, data);
        AllMemoId.Add(data.Id);
        //GameDataHandler.Instance.SaveData.AllGetMemoId = AllMemoId;
    }


    public void AddNewMemo(int id)
    {
        if(memoDataDict.ContainsKey(id)) return;

        var memoData = ItemControlHandler.Instance.GetMemoData(id);
        if(!memoData) return;
        
        memoDataDict.Add(id, memoData);
    }


    public MemoData GetTargetMemo(int id)
    {
        return memoDataDict.ContainsKey(id) ? memoDataDict[id] : null;
    }
}
