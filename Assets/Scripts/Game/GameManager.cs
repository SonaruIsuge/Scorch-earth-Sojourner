
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private RoomsController roomsController;


    private void OnEnable()
    {
        uiManager.BindPlayerWithUI(player);
        roomsController.OnRoomChange += uiManager.ChangeRoomText;
    }


    private void OnDisable()
    {
        uiManager.UnbindPlayerWithUI(player);
        roomsController.OnRoomChange -= uiManager.ChangeRoomText;
    }


    private void Start()
    {
        player.InitPlayer();
        uiManager.LateBindUI(player);
    }


    private void Update()
    {
        player.UpdatePlayer();   
    }
}
