

using SonaruUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowHandler : TSingletonMonoBehaviour<GameFlowHandler>
{
    public SceneData SceneData { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
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
