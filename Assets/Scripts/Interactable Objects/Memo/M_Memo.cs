using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Memo : MonoBehaviour, IInteractable
{
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    [field:SerializeField] public string interactHint { get; private set; }
    [SerializeField] private MemoData memoData;

    private IMaterialSwitcher materialSwitcher;


    private void Awake()
    {
        materialSwitcher = GetComponent<IMaterialSwitcher>();
    }
    
    
    public void OnSelect()
    {
        isSelect = true;
        materialSwitcher.ReplaceMaterial();
    }

    public void Interact(Player player)
    {
        // add memo to album book
        if (player.AlbumBook)
        {
            player.AlbumBook.GetNewMemo(memoData);
            
            AudioHandler.Instance.SpawnAudio(AudioType.PickPaper, stopLast: true);
        }

        gameObject.SetActive(false);
    }

    public void OnDeselect()
    {
        isSelect = false;
        materialSwitcher.ResetMaterial();
    }
}
