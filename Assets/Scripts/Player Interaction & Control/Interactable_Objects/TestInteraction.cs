using UnityEngine;
using Object = System.Object;

public class TestInteraction : MonoBehaviour,IInteractable
{
    public string InteractionPrompt { get; }
    public Canvas InteractionCanvas;
    
    public Dialogue dialogue;
    public bool Interact(Interactor interactor)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        
        InteractionCanvas.transform.position = interactor.transform.position + new Vector3(0, 0.5f, 0);
        
        Debug.Log("interacted with " + this.name);
        
        return true;
    }
}
