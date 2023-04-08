using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUI : MonoBehaviour
{
    private Player player;
    private RoomsController roomController;
    
    [SerializeField] private TMP_Text RoomText;
    [SerializeField] private List<GameObject> AllGeneralUI;


    public void EnableGeneralUI(bool enable)
    {
        foreach(var ui in AllGeneralUI) ui.SetActive(enable);
    }
    
    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        roomController = FindObjectOfType<RoomsController>();
    }


    private void OnEnable()
    {
       roomController.OnRoomChange += ChangeCurrentRoomText;
    }


    private void OnDisable()
    {
        roomController.OnRoomChange -= ChangeCurrentRoomText;
    }


    private void ChangeCurrentRoomText(RoomData data)
    {
        RoomText.text = data.RoomName;
    }
}
