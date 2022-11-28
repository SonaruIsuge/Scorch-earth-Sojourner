using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsController : MonoBehaviour
{
    [SerializeField] private List<RoomData> AllRoom;
    private Dictionary<Room, RoomData> allRoomDict;
    [field: SerializeField] public Room CurrentRoom { get; private set; }

    public event Action<RoomData> OnRoomChange;


    private void Awake()
    {
        allRoomDict = new Dictionary<Room, RoomData>();
        foreach(var room in AllRoom) allRoomDict.Add(room.roomType, room);
    }


    private void Start()
    {
        // set start room and thus invoke event.
        ChangeRoom(Room.EmptyWarehouse);
    }


    public void ChangeRoom(Room changeRoom)
    {
        if(!allRoomDict.ContainsKey(changeRoom)) return;
        CurrentRoom = changeRoom;
        OnRoomChange?.Invoke(allRoomDict[CurrentRoom]);
    }
}
