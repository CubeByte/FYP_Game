using CharacterData;
using Combat_Action;
using UnityEngine;

public class Water_Interaction : MonoBehaviour, IInteractable
{
    public string InteractionPrompt { get; }
    public Canvas dialogueCanvas;
    public Dialogue dialogue;
    public GameObject playerObject;

    [Header("Player Data")]
    public PlayerPersistantData playerPersistantData;
    public int targetPlayerIndex = 0;
    public int replaceActionSlot = 0;

    [Header("Action Asset")]
    public CombatAction waterAction;

    private const int InitialStep = 1;
    private const int LearnStep = 5;
    private int interactionStep = InitialStep;
    
    public bool Interact(Interactor interactor)
    {
        if (!dialogueCanvas.isActiveAndEnabled)
        {
            OpenDialogue();
        }
        else
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            
            if (interactionStep == LearnStep)
            {
                NewScriptableObjectScript.setIsKnown("water");
                
                GiveWaterActionToPlayer();
                
                ExplorationPlayerState.Save(playerObject.transform);
                Go_To_Battle_Interaction.LoadBattleFor(interactor, "First_Encounter");
            }

            interactionStep++;
        }

        return true;
    }

    void GiveWaterActionToPlayer()
    {
        CombatAction[] actions = playerPersistantData.characters[targetPlayerIndex].combatActions;

        if (actions != null && replaceActionSlot >= 0 && replaceActionSlot < actions.Length)
        {
            actions[replaceActionSlot] = waterAction;
        }
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