using UnityEngine;
using Object = System.Object;

public class TestInteraction : MonoBehaviour,IInteractable
{
    public string InteractionPrompt { get; }
    public Canvas dialogueCanvas;
    public Dialogue dialogue;
    public bool Interact(Interactor interactor)
    {
        if (dialogueCanvas.isActiveAndEnabled == false)
        {
            Debug.Log("Started conversation with " + this.name);
            OpenDialogue();
        }
        else
        {
            Debug.Log("Continued conversation with " + this.name);
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
        return true;
    }

    void OpenDialogue()
    {
        dialogueCanvas.enabled = true;
        
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        
        dialogueCanvas.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        
    }
}
