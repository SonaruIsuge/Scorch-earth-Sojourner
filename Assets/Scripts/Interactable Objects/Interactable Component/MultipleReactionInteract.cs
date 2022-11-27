using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleReactionInteract : MonoBehaviour, IInteractable
{
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    [field: SerializeField] public string interactHint { get; private set; }
    
    private IMaterialSwitcher materialSwitcher;
    private IInteractReact[] reactions;


    private void Awake()
    {
        materialSwitcher = GetComponent<IMaterialSwitcher>();
        reactions = GetComponents<IInteractReact>();
    }


    public void OnSelect()
    {
        isSelect = true;
        materialSwitcher?.ReplaceMaterial();
    }

    public void Interact(Player player)
    {
        foreach(var reaction in reactions) reaction.React(player);
    }

    public void OnDeselect()
    {
        isSelect = false;
        materialSwitcher?.ResetMaterial();
    }
}
