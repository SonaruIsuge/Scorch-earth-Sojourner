﻿using System;
using UnityEngine;
using UnityEngine.Events;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GeneralUI generalUI;
    [SerializeField] private AlbumBookUI albumBookUI;
    [SerializeField] private MemoUI memoUI;
    [SerializeField] private MemoryCameraUI memoryCameraUI;
    [SerializeField] private ProjectorUI projectorUI;
    [SerializeField] private GameOverTransitionUI gameOverUI;
    [SerializeField] private TransitionUI transitionUI;


    private void OnEnable()
    {
        albumBookUI.OnEnableUI += ToggleGeneralUIEnable;
        memoryCameraUI.OnEnableUI += ToggleGeneralUIEnable;
        projectorUI.OnEnableUI += ToggleGeneralUIEnable;
        
    }


    private void OnDisable()
    {
        albumBookUI.OnEnableUI -= ToggleGeneralUIEnable;
        memoryCameraUI.OnEnableUI -= ToggleGeneralUIEnable;
        projectorUI.OnEnableUI -= ToggleGeneralUIEnable;
    }


    public void BindPlayerWithUI(Player player)
    {
        player.OnPropEquipped += albumBookUI.RegisterAlbumBookUI;
        player.OnPropEquipped += memoUI.RegisterMemoUI;
        player.OnPropEquipped += memoryCameraUI.RegisterMemoryCameraUI;
    }


    public void LateBindUI(Player player)
    {
        player.InteractHandler.OnItemInteract += projectorUI.ChangeTargetProjector;
    }


    public void UnbindPlayerWithUI(Player player)
    {
        player.OnPropEquipped -= albumBookUI.RegisterAlbumBookUI;
        player.OnPropEquipped -= memoUI.RegisterMemoUI;
        player.OnPropEquipped -= memoryCameraUI.RegisterMemoryCameraUI;
        player.InteractHandler.OnItemInteract -= projectorUI.ChangeTargetProjector;
    }


    public async void GameStartUI()
    {
        await transitionUI.FadeOut();
    }


    private void ToggleGeneralUIEnable(bool otherUIEnable)
    {
        generalUI.EnableGeneralUI(!otherUIEnable);
    }


    public void ChangeRoomText(RoomData data)
    {
        generalUI.ChangeCurrentRoomText(data);
    }


    public void GameOverUI()
    {
        gameOverUI.ImageTransitionPerform();
    }


    public void BindGameOverBtn(UnityAction onClick) => gameOverUI.BindExitBtn(onClick);
}
