using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Memo", menuName = "Memo System/Memo")]
public class MemoData : ScriptableObject
{
    public int Id;
    public string Title;
    [TextArea(5, 10)]
    public string Content;
}
