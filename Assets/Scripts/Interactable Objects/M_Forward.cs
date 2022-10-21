using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class M_Forward : MonoBehaviour, IInteractable
{
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    
    [field:SerializeField] public string interactHint { get; private set; }


    private SpriteRenderer spriteRenderer;
    private Animator forwardAni;
    private float aniTimer;
    
    [SerializeField]private bool forwardEnable;
    private readonly int OutlineThickness = Shader.PropertyToID("_OutlineThickness");


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        forwardAni = GetComponent<Animator>();
        aniTimer = 0;
        
        forwardEnable = false;
    }

    void Update()
    {
        aniTimer += Time.deltaTime;
    }

    public void OnSelect()
    {
        isSelect = true;
        spriteRenderer.material.SetFloat(OutlineThickness, 1.0f);
    }

    public void Interact()
    {
        if (aniTimer <= forwardAni.GetCurrentAnimatorStateInfo(0).length) return;
        
        forwardEnable = !forwardEnable;
        forwardAni.SetBool(AnimatorParam.LeverOn, forwardEnable);
        aniTimer = 0;
    }

    public void OnDeselect()
    {
        isSelect = false;
        spriteRenderer.material.SetFloat(OutlineThickness, 0);
    }

    public bool ForwardEnable => forwardEnable;
    
}
