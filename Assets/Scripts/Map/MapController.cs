using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class MapController : MonoBehaviour
{
    [SerializeField] private RectTransform mapPanel;
    [SerializeField] private Transform playerMapIcon;
    [SerializeField] private Vector2 showMapPos;
    [SerializeField] private Vector2 hideMapPos;

    private Dictionary<Room, Transform> roomPosInMap;
    private Tweener mapTween;

    private bool isOpenMap;


    public void GenerateMapData()
    {
        roomPosInMap = new Dictionary<Room, Transform>();
        
        var allRoomPosInMap = mapPanel.GetComponentsInChildren<PosInMap>();
        foreach (var mapPos in allRoomPosInMap)
        {
            roomPosInMap.Add(mapPos.room, mapPos.transform);
        }
    }


    public void ToggleMap()
    {
        isOpenMap = !isOpenMap;

        mapTween?.Kill();
        mapTween = mapPanel.DOAnchorPos(isOpenMap ? showMapPos : hideMapPos, 0.5f);
        mapTween.SetEase(isOpenMap ? Ease.OutBack : Ease.InBack);
        mapTween.Play();

        AudioHandler.Instance.SpawnAudio(AudioType.ToggleMap);
    }


    public void ChangeCurrentPos(RoomData roomData)
    {
        if(!roomPosInMap.ContainsKey(roomData.roomType)) return;

        playerMapIcon.position = roomPosInMap[roomData.roomType].position;
    }
}
