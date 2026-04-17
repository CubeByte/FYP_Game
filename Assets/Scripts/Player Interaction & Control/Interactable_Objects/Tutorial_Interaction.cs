using CharacterData;
using UnityEngine;

namespace Player_Interaction___Control.Interactable_Objects
{
    public class Tutorial_Interaction:MonoBehaviour,IInteractable
    {
        
        public string InteractionPrompt { get; }
        public Canvas dialogueCanvas;
        public Dialogue dialogue;
        public GameObject playerObject;

        [Header("Player Data")]
        public PlayerPersistantData playerPersistantData;
        public int targetPlayerIndex = 0;
        public bool autoEquipOnLearn = true;
        public int autoEquipSlot = 0;

        [Header("Action Asset")]
        public CombatAction healAction;
        
        private const int InitialStep = 1;
        private const int LearnStep = 5;
        private int interactionStep = InitialStep;
        
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
                    NewScriptableObjectScript.setIsKnown("heal");
                    Debug.Log("learned Heal");
                    
                }
                if (interactionStep == 7)
                {
                    PlayerActionUtility.LearnAction(playerPersistantData, targetPlayerIndex, healAction);

                    if (autoEquipOnLearn)
                    {
                        PlayerActionUtility.EquipAction(playerPersistantData, targetPlayerIndex, autoEquipSlot, healAction);
                    }
                    
                    ExplorationPlayerState.Save(playerObject.transform);
                    Go_To_Battle_Interaction.LoadBattleFor(interactor, "Tutorial_Fight",
                        "You get overcome with a weird sensation...");
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