using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class M_TurnRight : MonoBehaviour, IInteractable
{
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    [field:SerializeField]public string interactHint { get; private set; }
    
    private SpriteRenderer spriteRenderer;
    private Animator rightAni;
    private float aniTimer;

    [SerializeField]private bool rightEnable;
    private readonly int OutlineThickness = Shader.PropertyToID("_OutlineThickness");
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rightAni = GetComponent<Animator>();
        
        rightEnable = false;
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
        if (aniTimer <= rightAni.GetCurrentAnimatorStateInfo(0).length) return;
        
        rightEnable = !rightEnable;
        rightAni.SetBool(AnimatorParam.LeverOn, rightEnable);
        aniTimer = 0;
    }

    public void OnDeselect()
    {
        isSelect = false;
        spriteRenderer.material.SetFloat(OutlineThickness, 0);
    }

    public bool RightEnable => rightEnable;
}
