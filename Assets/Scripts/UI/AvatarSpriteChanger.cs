using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSpriteChanger : MonoBehaviour
{
    [SerializeField] private RawImage avatarImage;
    [SerializeField] private Texture defaultAvatarTex;
    [SerializeField] private Texture alternativeAvatarTex;
    [SerializeField] private List<Room> ChangeImageRooms;

    private RoomsController roomsController;

    private void Awake()
    {
        roomsController = FindObjectOfType<RoomsController>();
    }


    private void OnEnable()
    {
        roomsController.OnRoomChange += ChangeAvatar;
    }


    private void OnDisable()
    {
        roomsController.OnRoomChange -= ChangeAvatar;
    }


    private void ChangeAvatar(RoomData roomData)
    {
        if (ChangeImageRooms.Contains(roomData.roomType)) avatarImage.texture = alternativeAvatarTex;
        else avatarImage.texture = defaultAvatarTex;
    }
}
