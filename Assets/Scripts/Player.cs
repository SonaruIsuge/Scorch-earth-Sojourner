using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public IInput PlayerInput;
    [SerializeField] private PlayerMove playerMove;

    public float MoveSpeed;

    void Awake()
    {
        PlayerInput = GetComponent<IInput>();        
        playerMove = new PlayerMove(this);
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMove.Move();
    }
}
