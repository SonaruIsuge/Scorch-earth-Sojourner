using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Projector : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;

    private Player interactPlayer;
    
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    
    [field:SerializeField] public string interactHint { get; private set; }
    private readonly int OutlineThickness = Shader.PropertyToID("_OutlineThickness");
    
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    
    public void OnSelect()
    {
        isSelect = true;
        spriteRenderer.material.SetFloat(OutlineThickness, 1.0f);
    }

    public void Interact(Player player)
    {
        interactPlayer = player;

        if (player.AlbumBook)
        {
            player.ChangePropState(UsingProp.AlbumBook);
        }
    }

    public void OnDeselect()
    {
        isSelect = false;
        interactPlayer = null;
        spriteRenderer.material.SetFloat(OutlineThickness, 0);
    }
}
