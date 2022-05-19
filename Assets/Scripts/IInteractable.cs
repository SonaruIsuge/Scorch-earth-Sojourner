

public interface IInteractable
{
    bool isInteract { get; }
    bool isSelect { get; }

    void OnSelect();
    void Interact();
    void OnDeselect();
}