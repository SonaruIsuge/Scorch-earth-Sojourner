
using SonaruUtilities;
using UnityEngine;

public sealed class ItemControlHandler : TSingletonMonoBehaviour<ItemControlHandler>
{
    [SerializeField] private RecordableInventory inventory;

    protected override void Awake()
    {
        base.Awake();
        
        inventory.InitItemDict();
    }
    
    
    
}
