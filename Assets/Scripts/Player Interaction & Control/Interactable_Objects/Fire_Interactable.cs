using UnityEngine;

namespace Player_Interaction___Control.Interactable_Objects
{
    public class Fire_Interactable :MonoBehaviour,IInteractable
    {
        
        public string InteractionPrompt { get; }
        public Canvas dialogueCanvas;
        public Dialogue dialogue;
        public GameObject playerObject;

        private const int InitialStep = 1;
        private const int LearnStep = 4;
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
                    NewScriptableObjectScript.setIsKnown("fire");
                    Debug.Log("learned fire");
                }

                if (interactionStep == 4)
                {
                    ExplorationPlayerState.Save(playerObject.transform);
                    Go_To_Battle_Interaction.LoadBattleFor(interactor, "Second_Encounter", "You and the stick bug Prepare for a steamy fight");
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
