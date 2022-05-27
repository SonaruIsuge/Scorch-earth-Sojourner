using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class M_TurnRight : MonoBehaviour, IInteractable
{
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    
    private SpriteRenderer spriteRenderer;
    private Animator rightAni;
    private float aniTimer;

    [SerializeField]private bool rightEnable;
    
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
        var color = spriteRenderer.color;
        color = new Color(color.r, color.g, color.b, 1);
        spriteRenderer.color = color;
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
        var color = spriteRenderer.color;
        color = new Color(color.r, color.g, color.b, 0.75f);
        spriteRenderer.color = color;
    }

    public bool RightEnable => rightEnable;
}
