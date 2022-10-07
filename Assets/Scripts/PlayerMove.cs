
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
    [SerializeField] private Transform playerBody;
    private float previousDir;
    private bool isStopMove;

    void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        playerAni = playerBody.GetComponent<Animator>();
        
        previousDir = 1;
    }

    public void UpdateMove(float horizontal, float vertical)
    {
        if(isStopMove) return;
        if (movement.x != 0) previousDir = movement.x;
        
        Move(horizontal, vertical);
        Flip(checkFlip());
    }

    public void EnableMove(bool enable)
    {
        isStopMove = !enable;
        if(!enable) ForceStopMove();
    }

    private void ForceStopMove()
    {
        movement = Vector2.zero;
        if(rb) rb.velocity = Vector2.zero;
        playerAni.SetBool(AnimatorParam.Move, false);
    }
    
    private void Move(float horizontal, float vertical)
    {
        movement.x = horizontal;
        movement.y = vertical;
        if(rb) rb.velocity = MoveSpeed * movement;
        playerAni.SetBool(AnimatorParam.Move, movement != Vector2.zero);
        playerAni.SetFloat(AnimatorParam.Direction, movement.x);
    }

    private bool checkFlip() => previousDir < 0;

    private void Flip(bool isReverse)
    {
        var playerRotation = transform.localRotation;
        playerRotation.y = isReverse ? 180 : 0;
        playerBody.rotation = playerRotation;
    }
}
