
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private RoomsController roomsController;
    [SerializeField] private GameGoal gameGoal;
    [SerializeField] private MapController mapController;

    private void OnEnable()
    {
        uiManager.BindPlayerWithUI(player);
        roomsController.OnRoomChange += uiManager.ChangeRoomText;
        roomsController.OnRoomChange += mapController.ChangeCurrentPos;
        gameGoal.OnTriggerGameOver += GameOver;
        player.OnPlayerToggleMap += mapController.ToggleMap;
    }


    private void OnDisable()
    {
        uiManager.UnbindPlayerWithUI(player);
        roomsController.OnRoomChange -= uiManager.ChangeRoomText;
        roomsController.OnRoomChange -= mapController.ChangeCurrentPos;
        gameGoal.OnTriggerGameOver -= GameOver;
        player.OnPlayerToggleMap -= mapController.ToggleMap;
    }


    private void Start()
    {
        uiManager.GameStartUI();
        
        player.InitPlayer();
        uiManager.LateBindUI(player);
        
        mapController.GenerateMapData();
        
        AudioHandler.Instance.ChangeBgm(AudioType.GameBgm, 0.1f);
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
        player.ResetPlayerEquipment();
        await Task.Delay(500);
        
        // Play goal animation
        await gameGoal.GameOverAni();

        // Show game over UI
        uiManager.BindGameOverBtn(ReturnMainMenu);
        uiManager.GameOverUI();
    }


    /// <summary>
    /// Clear all photo and return to main menu.
    /// </summary>
    private void ReturnMainMenu()
    {
        PhotoSaveLoadHandler.Instance.ClearAllData();
        GameFlowHandler.Instance.LoadScene(0, null);
    }
}
