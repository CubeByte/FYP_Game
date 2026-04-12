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
                    NewScriptableObjectScript.setIsKnown("blunt");
                    Debug.Log("learned blunt");
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
