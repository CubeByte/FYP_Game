using CharacterData;
using UnityEngine;

namespace Player_Interaction___Control.Interactable_Objects
{
    public class Blunt_sign_interaction:MonoBehaviour,IInteractable
    {
        
        public string InteractionPrompt { get; }
        public Canvas dialogueCanvas;
        public Dialogue dialogue;

        private const int InitialStep = 1;
        private const int LearnStep = 4;
        private int interactionStep = InitialStep;
        
        [Header("Player Action Data")]
        public PlayerPersistantData playerPersistantData;
        public CombatAction bluntAction;
        public int targetPlayerIndex = 0;
        public bool autoEquipOnLearn = false;
        public int autoEquipSlot = 0;
    
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
                    NewScriptableObjectScript.setIsKnown("blunt");
                    Debug.Log("learned blunt");
                    
                    PlayerActionUtility.LearnAction(playerPersistantData, targetPlayerIndex, bluntAction);

                    if (autoEquipOnLearn)
                    {
                        PlayerActionUtility.EquipAction(playerPersistantData, targetPlayerIndex, autoEquipSlot, bluntAction);
                    }
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
