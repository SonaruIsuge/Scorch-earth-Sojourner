using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoomSwitcher
{
    SpriteRenderer changeRoom { get; }
    
    void SetOnRoomChange();
}