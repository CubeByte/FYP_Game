using CharacterData;
using UnityEngine;

namespace Player_Interaction___Control.Interactable_Objects
{
    public class Peirce_Interaction:MonoBehaviour, IInteractable
    {
        
        public string InteractionPrompt { get; }
        public Canvas dialogueCanvas;
        public Dialogue dialogue;
        public GameObject playerObject;

        private const int InitialStep = 1;
        private const int LearnStep = 5;
        private int interactionStep = InitialStep;
        
        [Header("Player Data")]
        public PlayerPersistantData playerPersistantData;
        public int targetPlayerIndex = 0;
        public bool autoEquipOnLearn = true;
        public int autoEquipSlot = 0;

        [Header("Action Asset")]
        public CombatAction pierceAction;
    
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
                    NewScriptableObjectScript.setIsKnown("pierce");
                    Debug.Log("learned pierce");
                    
                    PlayerActionUtility.LearnAction(playerPersistantData, targetPlayerIndex, pierceAction);

                    if (autoEquipOnLearn)
                    {
                        PlayerActionUtility.EquipAction(playerPersistantData, targetPlayerIndex, autoEquipSlot, pierceAction);
                    }
                }
                if (interactionStep == 6)
                {
                    ExplorationPlayerState.Save(playerObject.transform);
                    Go_To_Battle_Interaction.LoadBattleFor(interactor, "Training_encounter",
                        "You picked up a bow... try it out...");
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
}
