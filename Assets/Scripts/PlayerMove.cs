
using System;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    private Player player;
    private Rigidbody2D rb;
    private IInput input;

    [SerializeField]private Vector2 movement = Vector2.zero;

    private int xMoveDir;
    private int previousXDir;

    public PlayerMove(Player player)
    {
        this.player = player;
        input = player.PlayerInput;
        rb = player.GetComponent<Rigidbody2D>();

        xMoveDir = 0;
        previousXDir = 1;
    }

    public void Move()
    {
        xMoveDir = (int)Math.Sign(movement.x);
        
        if(xMoveDir != previousXDir) setReverseForward();
        
        movement.x = input.horizontal;
        movement.y = input.vertical;
        if(rb) rb.velocity = player.MoveSpeed * movement;
    }

    private void setReverseForward()
    {
        var playerTransform = player.transform;
        var playerScale = playerTransform.localScale;
        
        // if not move, use previous direction
        playerScale.x = xMoveDir == 0 ? previousXDir : xMoveDir;
        previousXDir = xMoveDir;
        playerTransform.localScale = playerScale;
    }
    
}
