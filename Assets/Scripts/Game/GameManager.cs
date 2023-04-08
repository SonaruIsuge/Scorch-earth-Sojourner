
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
    }


    private void OnDisable()
    {
        uiManager.UnbindPlayerWithUI(player);
    }


    private void Start()
    {
        uiManager.LateBindUI(player);
    }
}
