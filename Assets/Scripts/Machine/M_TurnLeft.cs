using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class M_TurnLeft : MonoBehaviour, IInteractable
{
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    [field:SerializeField]public string interactHint { get; private set; }
    
    private SpriteRenderer spriteRenderer;
    private Animator leftAni;
    private float aniTimer;

    [SerializeField]private bool leftEnable;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        leftAni = GetComponent<Animator>();
        
        leftEnable = false;
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
        if (aniTimer <= leftAni.GetCurrentAnimatorStateInfo(0).length) return;
        
        leftEnable = !leftEnable;
        leftAni.SetBool(AnimatorParam.LeverOn, leftEnable);
        aniTimer = 0;
    }

    public void OnDeselect()
    {
        isSelect = false;
        var color = spriteRenderer.color;
        color = new Color(color.r, color.g, color.b, 0.75f);
        spriteRenderer.color = color;
    }

    public bool LeftEnable => leftEnable;
}
