
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneralUI : MonoBehaviour
{
    [SerializeField] private TMP_Text RoomText;
    [SerializeField] private List<GameObject> AllGeneralUI;


    public void EnableGeneralUI(bool enable)
    {
        foreach(var ui in AllGeneralUI) ui.SetActive(enable);
    }


    public void ChangeCurrentRoomText(RoomData data)
    {
        RoomText.text = data.RoomName;
    }
}
