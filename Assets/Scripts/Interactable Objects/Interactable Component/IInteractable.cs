

public interface IInteractable
{
    bool isInteract { get; }
    bool isSelect { get; }
    string interactHint { get; }

    void OnSelect();
    void Interact(Player player);
    void OnDeselect();
}