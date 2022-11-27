using UnityEngine;


public class DialogueReaction : MonoBehaviour, IInteractReact
{
    [TextArea(3, 6)]
    [SerializeField] private string investigateSentence;
    
    
    public void React(Player player)
    {
        DialogueHandler.Instance.StartSentence(investigateSentence);
    }
}
