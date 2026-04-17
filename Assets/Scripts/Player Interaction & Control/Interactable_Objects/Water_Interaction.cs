using CharacterData;
using UnityEngine;

public class Water_Interaction : MonoBehaviour, IInteractable
{
    public string InteractionPrompt { get; }
    public Canvas dialogueCanvas;
    public Dialogue dialogue;
    public GameObject playerObject;

    [Header("Player Action Data")]
    public PlayerPersistantData playerPersistantData;
    public CombatAction waterAction;
    public int targetPlayerIndex = 0;
    public bool autoEquipOnLearn = true;
    public int autoEquipSlot = 0;

    private const int InitialStep = 1;
    private const int LearnStep = 3;
    private int interactionStep = InitialStep;
    
    public bool Interact(Interactor interactor)
    {
        if (!dialogueCanvas.isActiveAndEnabled)
        {
            Debug.Log("Started conversation with " + name);
            OpenDialogue();
        }
        else
        {
            Debug.Log("Continued conversation with " + name);
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            
            if (interactionStep == LearnStep)
            {
                NewScriptableObjectScript.setIsKnown("water");
                Debug.Log("learned water");

                PlayerActionUtility.LearnAction(playerPersistantData, targetPlayerIndex, waterAction);

                if (autoEquipOnLearn)
                {
                    PlayerActionUtility.EquipAction(playerPersistantData, targetPlayerIndex, autoEquipSlot, waterAction);
                }

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