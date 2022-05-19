using System;
using UnityEngine;


[Serializable]
public class PlayerInteractHandler
{
    public float InteractRange;
    
    private Player player;
    private IInteractable currentSelectObj;

    // ray return hit object
    // interact with the object

    public PlayerInteractHandler(Player player)
    {
        this.player = player;
        currentSelectObj = null;
    }

    public void Update()
    {
        Debug.DrawRay(player.transform.position, player.transform.right * InteractRange );
        DetectInteractableObj();
    }
    
    private void DetectInteractableObj()
    {
        var playerTrans = player.transform;
        var hits = Physics2D.RaycastAll(playerTrans.position, playerTrans.right, InteractRange);
        
    }
}
