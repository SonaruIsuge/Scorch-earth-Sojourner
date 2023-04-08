using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PowerRoomDark : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    private SpriteRenderer spiteRenderer;
    private Color darknessColor;
    private Tweener changeDarkTween;
    private KeyEventManager keyEventManager;
    
    
    private void Awake()
    {
        keyEventManager = FindObjectOfType<KeyEventManager>();
        spiteRenderer = GetComponent<SpriteRenderer>();
        darknessColor = spiteRenderer.color;
    }


    private void Update()
    {
        if (!keyEventManager) return;
        if(!keyEventManager.CheckKeyEventState(KeyEvent.EmergencyPowerOpened)) return;
        
        darknessColor.a = 0;
        spiteRenderer.DOColor(darknessColor, fadeTime);
        
    }
}
