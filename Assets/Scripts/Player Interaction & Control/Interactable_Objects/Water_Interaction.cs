using UnityEngine;
public class Water_Interaction : MonoBehaviour, IInteractable
{
    
    public string InteractionPrompt { get; }
    public Canvas dialogueCanvas;
    public Dialogue dialogue;
    public GameObject playerObject;

    private const int InitialStep = 1;
    private const int LearnStep = 3;
    private int interactionStep = InitialStep;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
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
            
            if (interactionStep == LearnStep)
            {
                NewScriptableObjectScript.setIsKnown("water");
                Debug.Log("learned water");
                ExplorationPlayerState.Save(playerObject.transform);
                Go_To_Battle_Interaction.LoadBattleFor(interactor, "First_Encounter");
            }
            interactionStep++;
        }
        return true;
    }

    public void ResetInteraction()
    {
        interactionStep = InitialStep;

        if (dialogueCanvas != null)
        {
            dialogueCanvas.enabled = false;
        }
    }

    void OpenDialogue()
    {
        dialogueCanvas.enabled = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        dialogueCanvas.transform.position = transform.position + new Vector3(0, 1f, 0);
        
    }
}
