using UnityEngine;

namespace Player_Interaction___Control.Interactable_Objects
{
    public class Blunt_sign_interaction:MonoBehaviour,IInteractable
    {
        
        public string InteractionPrompt { get; }
        public Canvas dialogueCanvas;
        public Dialogue dialogue;

        private int i = 1;
        private int x = 4;
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
            
                if (i == x)
                {
                    NewScriptableObjectScript.setIsKnown("blunt");
                    Debug.Log("learned blunt");
                }
                i++;
            }
            return true;
        }
        void OpenDialogue()
        {
            dialogueCanvas.enabled = true;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            dialogueCanvas.transform.position = transform.position + new Vector3(0, 1f, 0);
        
        }
    }
}