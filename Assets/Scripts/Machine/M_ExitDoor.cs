
using UnityEngine;


public class M_ExitDoor : MonoBehaviour, IInteractable
{
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    [field:SerializeField] public string interactHint { get; private set; }
    public void OnSelect()
    {
        isSelect = true;
    }

    public void Interact()
    {
        
    }

    public void OnDeselect()
    {
        isSelect = false;
    }
}
