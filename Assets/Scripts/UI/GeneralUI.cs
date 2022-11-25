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
    
    [SerializeField] private Button InteractBtn;
    [SerializeField] private TMP_Text RoomText;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        roomController = FindObjectOfType<RoomsController>();
    }


    private void OnEnable()
    {
       InteractBtn.onClick.AddListener(OnInteractBtnClick);
       roomController.OnRoomChange += ChangeCurrentRoomText;
    }


    private void OnDisable()
    {
        InteractBtn.onClick.RemoveAllListeners();
        roomController.OnRoomChange -= ChangeCurrentRoomText;
    }


    private void OnInteractBtnClick()
    {
        if(player) player.InteractHandler.Interact();
    }


    private void ChangeCurrentRoomText(RoomData data)
    {
        RoomText.text = data.RoomName;
    }
}
