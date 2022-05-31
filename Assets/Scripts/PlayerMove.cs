
using System;
using Tools;
using UnityEngine;

[Serializable]
public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;
    
    private Player player;
    private Rigidbody2D rb;
    private Animator playerAni;

    [SerializeField]private Vector2 movement = Vector2.zero;
    private float previousDir;

    void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        playerAni = GetComponent<Animator>();
        
        previousDir = 1;
    }

    void Update()
    {
        if (movement.x != 0) previousDir = movement.x;
        
        Move();
        Flip(checkFlip());
    }
    
    private void Move()
    {
        movement.x = player.GetInput().horizontal;
        movement.y = player.GetInput().vertical;
        if(rb) rb.velocity = MoveSpeed * movement;
        playerAni.SetBool(AnimatorParam.Move, movement != Vector2.zero);
        playerAni.SetFloat(AnimatorParam.Direction, movement.x);
    }

    private bool checkFlip() => previousDir < 0;

    private void Flip(bool isReverse)
    {
        var playerRotation = transform.localRotation;
        playerRotation.y = isReverse ? 180 : 0;
        transform.rotation = playerRotation;
    }
}
