

using System;
using System.Collections.Generic;
using SonaruUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowHandler : TSingletonMonoBehaviour<GameFlowHandler>
{
    private Dictionary<KeyEvent, bool> LevelProgressEvents;
    
    public SceneData SceneData { get; private set; }

    
    protected override void Awake()
    {
        base.Awake();

        LevelProgressEvents = new Dictionary<KeyEvent, bool>()
        {
            {KeyEvent.WarehouseKeyGet, false},
            {KeyEvent.DoorEatenBySlime, false},
            {KeyEvent.EmergencyPowerOpened, false},
        };
    }
    
    
    public void LoadScene(SceneIndex sceneToLoad, SceneData data)
    {
        SceneManager.LoadScene((int)sceneToLoad);
        SceneData = data;
    }
}


public abstract class SceneData {}


public class OutDoorData : SceneData
{
    public Vector2 MachinePosition;
    public float MachineRotation;

    public OutDoorData(Vector2 pos, float rot)
    {
        MachinePosition = pos;
        MachineRotation = rot;
    }
}

public class LevelData : SceneData
{
    
}