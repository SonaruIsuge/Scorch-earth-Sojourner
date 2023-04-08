using UnityEngine;


public class ConditionInteract : MonoBehaviour, IInteractable
{
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    [field: SerializeField] public string interactHint { get; private set; }

    [SerializeField] private KeyEvent condition;
    
    private IMaterialSwitcher materialSwitcher;
    private IInteractReact defaultReaction;
    private IInteractReact conditionClearReaction;

    private KeyEventManager keyEventManager;

    private void Awake()
    {
        keyEventManager = FindObjectOfType<KeyEventManager>();
        materialSwitcher = GetComponent<IMaterialSwitcher>();
        var allReaction = GetComponents<IInteractReact>();

        if (allReaction.Length < 2)
        {
            defaultReaction = null;
            conditionClearReaction = null;
        }
        else
        {
            defaultReaction = allReaction[0];
            conditionClearReaction = allReaction[1];
        }

    }
    
    
    public void OnSelect()
    {
        isSelect = true;
        materialSwitcher?.ReplaceMaterial();

    }

    public void Interact(Player player)
    {
        if(!keyEventManager || !keyEventManager.CheckKeyEventState(condition)) defaultReaction?.React(player);
        else conditionClearReaction?.React(player);
    }

    public void OnDeselect()
    {
        isSelect = false;
        materialSwitcher?.ResetMaterial();
    }
}
