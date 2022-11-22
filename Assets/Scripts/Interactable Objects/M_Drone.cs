using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Drone : MonoBehaviour, IInteractable
{
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    [field: SerializeField] public string interactHint { get; private set; }
    
    [TextArea(2, 5)] [SerializeField] private string investigateSentence;
    
    private IMaterialSwitcher materialSwitcher;
    
    
    private void Awake()
    {
        materialSwitcher = GetComponent<IMaterialSwitcher>();
    }
    
    
    public void OnSelect()
    {
        isSelect = true;
        materialSwitcher.ReplaceMaterial();
    }

    public void Interact(Player player)
    {
        DialogueHandler.Instance.StartSentence(investigateSentence);
    }

    public void OnDeselect()
    {
        isSelect = false;
        materialSwitcher.ResetMaterial();
    }
}
