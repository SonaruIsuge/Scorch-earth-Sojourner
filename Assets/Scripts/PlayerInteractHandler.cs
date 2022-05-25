using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerInteractHandler : MonoBehaviour
{
    [SerializeField]private Transform interactPoint;
    public float InteractRange;
    
    private Player player;
    private IInteractable currentSelectObj;

    // ray return hit object
    // interact with the object

    void Awake()
    {
        player = GetComponent<Player>();
        currentSelectObj = null;
    }

    void Update()
    {
        currentSelectObj?.OnDeselect();
        DetectInteractableObj();
        currentSelectObj?.OnSelect();

        if (currentSelectObj != null && player.GetInput().interact)
        {
            currentSelectObj.Interact();
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawSphere(interactPoint.position, InteractRange);
    // }

    private void DetectInteractableObj()
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
    }
}
