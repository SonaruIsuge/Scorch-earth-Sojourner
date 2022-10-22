
using UnityEngine;


public class M_ExitDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private MachineControl machineControl;
    
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    [field:SerializeField] public string interactHint { get; private set; }
    public void OnSelect()
    {
        isSelect = true;
    }

    public void Interact(Player player)
    {
        GameFlowHandler.Instance.LoadScene(SceneIndex.Outdoor, new OutDoorData(machineControl.MachinePosInWorld, machineControl.RotateAngle));
    }

    public void OnDeselect()
    {
        isSelect = false;
    }
}
