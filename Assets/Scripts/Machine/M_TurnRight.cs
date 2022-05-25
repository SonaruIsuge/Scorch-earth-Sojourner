using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_TurnRight : MonoBehaviour, IInteractable
{
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    
    private SpriteRenderer spriteRenderer;

    [SerializeField]private bool rightEnable;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        rightEnable = false;
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
        rightEnable = !rightEnable;
    }

    public void OnDeselect()
    {
        isSelect = false;
        var color = spriteRenderer.color;
        color = new Color(color.r, color.g, color.b, rightEnable ? 0.8f : 0.4f);
        spriteRenderer.color = color;
    }

    public bool RightEnable => rightEnable;
}
