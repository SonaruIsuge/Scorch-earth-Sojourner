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
    [SerializeField] private RectTransform UIBackground;
    [SerializeField] private List<GameObject> AllGeneralUI;


    public void EnableGeneralUI(bool enable)
    {
        UIBackground.gameObject.SetActive(!enable);
        foreach(var ui in AllGeneralUI) ui.SetActive(enable);
    }
    
    
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
