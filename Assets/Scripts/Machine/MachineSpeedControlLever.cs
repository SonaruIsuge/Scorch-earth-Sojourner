using System;
using UnityEngine;

namespace Interact_Item
{
    public class MachineSpeedControlLever : MonoBehaviour, IInteractable
    {
        public bool isInteract { get; private set; }
        public bool isSelect { get; private set; }

        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnSelect()
        {
            var color = spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, 1);
            spriteRenderer.color = color;
        }

        public void Interact()
        {
        
        }

        public void OnDeselect()
        {
            var color = spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, 0.5f);
            spriteRenderer.color = color;
        }
    }
}