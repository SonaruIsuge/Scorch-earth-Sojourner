
using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private RoomsController roomsController;
    [SerializeField] private GameGoal gameGoal;

    private void OnEnable()
    {
        uiManager.BindPlayerWithUI(player);
        roomsController.OnRoomChange += uiManager.ChangeRoomText;
        gameGoal.OnGameOverProgress += GameOver;
    }


    private void OnDisable()
    {
        uiManager.UnbindPlayerWithUI(player);
        roomsController.OnRoomChange -= uiManager.ChangeRoomText;
        gameGoal.OnGameOverProgress -= GameOver;
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


    private async void GameOver()
    {
        // Disable player move and interact behavior
        player.PlayerMove.EnableMove(false);
        player.InteractHandler.EnableInteract(false);
        await Task.Delay(500);
        
        // Play goal animation
        gameGoal.GameOverAni();
        await Task.Delay(500);
        
        // Show game over UI
        uiManager.GameOverUI();
    }
}
