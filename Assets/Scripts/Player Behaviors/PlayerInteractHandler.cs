using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public class PlayerInteractHandler : MonoBehaviour
{
    [SerializeField] private Transform interactPoint;
    [SerializeField] private TMP_Text interactHint;
    [SerializeField] private float interactRange;
    
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
    

    private IInteractable DetectInteractableObj()
    {
        currentSelectObj = null;
        var hits = new Collider2D[10];
        Physics2D.OverlapCircleNonAlloc(interactPoint.position, interactRange, hits);
        var minDistance = Mathf.Infinity;
        foreach (var hit in hits)
        {
            if (!hit) continue;
            var hasInteract = hit.TryGetComponent<IInteractable>(out var interact);
            if (!hasInteract) continue;
            
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
