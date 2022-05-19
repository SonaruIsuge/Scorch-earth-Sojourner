
using System;
using UnityEngine;

[Serializable]
public class PlayerMove
{
    private Player player;
    private Rigidbody2D rb;
    private IInput input;

    [SerializeField]private Vector2 movement = Vector2.zero;
    
    private float previousDir;

    public PlayerMove(Player player)
    {
        this.player = player;
        input = player.PlayerInput;
        rb = player.GetComponent<Rigidbody2D>();
        
        previousDir = 1;
    }

    public void Move()
    {
        if (movement.x != 0) previousDir = movement.x;
        
        movement.x = input.horizontal;
        movement.y = input.vertical;
        if(rb) rb.velocity = player.MoveSpeed * movement;

        Flip(checkFlip());
    }

    private bool checkFlip() => previousDir < 0;

        private void Flip(bool isReverse)
    {
        var playerTransform = player.transform;
        var playerRotation = playerTransform.localRotation;
        playerRotation.y = isReverse ? 180 : 0;
        playerTransform.rotation = playerRotation;
    }
}
