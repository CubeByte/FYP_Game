using UnityEngine;

namespace Player_Interaction___Control.Interactable_Objects
{
    public class Fire_Interactable :MonoBehaviour,IInteractable
    {
        
        public string InteractionPrompt { get; }
        public Canvas dialogueCanvas;
        public Dialogue dialogue;
        public GameObject playerObject;

        private int i = 1;
        private int x = 3;
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
                    NewScriptableObjectScript.setIsKnown("fire");
                    Debug.Log("learned fire");
                    ExplorationPlayerState.Save(playerObject.transform);
                    Go_To_Battle_Interaction.LoadBattleFor(interactor, "Second_Encounter");
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