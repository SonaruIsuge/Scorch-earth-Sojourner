using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteract : MonoBehaviour, IInteractable
{
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    [field: SerializeField] public string interactHint { get; private set; }

    private IMaterialSwitcher materialSwitcher;
    private IInteractReact reaction;
        
    
    private void Awake()
    {
        materialSwitcher = GetComponent<IMaterialSwitcher>();
        reaction = GetComponent<IInteractReact>();
    }


    public void OnSelect()
    {
        isSelect = true;
        materialSwitcher.ReplaceMaterial();
    }

    public void Interact(Player player)
    {
        reaction.React(player);
    }

    public void OnDeselect()
    {
        isSelect = false;
        materialSwitcher.ResetMaterial();
    }
}
