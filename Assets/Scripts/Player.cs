using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public IInput PlayerInput;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private PlayerInteractHandler interactHandler;

    public float MoveSpeed;

    void Awake()
    {
        PlayerInput = new PlayerInputSystemInput();        
        playerMove = new PlayerMove(this);
        interactHandler = new PlayerInteractHandler(this);
    }

    void OnEnable()
    {
        PlayerInput.Create();
    }

    void OnDisable()
    {
        PlayerInput.Destroy();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput.ReadInput();
        playerMove.Move();
        interactHandler.Update();
    }
}
