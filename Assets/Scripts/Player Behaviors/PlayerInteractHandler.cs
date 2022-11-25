using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[Serializable]
public class PlayerInteractHandler : MonoBehaviour
{
    [SerializeField]private Transform interactPoint;
    [SerializeField]private TMP_Text interactHint;
    public float InteractRange;
    
    private Player player;
    public IInteractable currentSelectObj { get; private set; }
    private bool enableInteract;
    
    public event Action<IInteractable> OnItemInteract;

    void Awake()
    {
        player = GetComponent<Player>();
        currentSelectObj = null;
        enableInteract = true;
    }


    public void EnableInteract(bool enable)
    {
        enableInteract = enable;
    }
    

    public void UpdateSelect()
    {
        if(!enableInteract) return;
        
        currentSelectObj?.OnDeselect();
        ShowInteractHint(DetectInteractableObj());
        currentSelectObj?.OnSelect();
    }

    public void Interact()
    {
        if(!enableInteract) return;
        
        currentSelectObj?.Interact(player);
        OnItemInteract?.Invoke(currentSelectObj);
    }
    
    
    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawSphere(interactPoint.position, InteractRange);
    // }

    private IInteractable DetectInteractableObj()
    {
        currentSelectObj = null;
        var hits = Physics2D.OverlapCircleAll(interactPoint.position, InteractRange);
        var minDistance = Mathf.Infinity;
        foreach (var hit in hits)
        {
            var interact = hit.GetComponent<IInteractable>();
            if (interact == null) continue;
            
            //find the closest interactable object
            var diff = transform.position - hit.transform.position;
            var distance = diff.sqrMagnitude;
            
            if (!(distance < minDistance)) continue;
            
            minDistance = distance;
            currentSelectObj = interact;
        }

        return currentSelectObj;
    }


    private void ShowInteractHint(IInteractable interactable = null)
    {
        interactHint.text = interactable == null ? "" : $"(E) {interactable.interactHint}";
    }
}
