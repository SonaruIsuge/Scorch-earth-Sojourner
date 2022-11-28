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
    
    private void Awake()
    {
        spiteRenderer = GetComponent<SpriteRenderer>();
        darknessColor = spiteRenderer.color;
    }


    private void Update()
    {
        if (GameFlowHandler.Instance.CheckKeyEvent(KeyEvent.EmergencyPowerOpened))
        {
            darknessColor.a = 0;
            spiteRenderer.DOColor(darknessColor, fadeTime);
        }
    }
}
