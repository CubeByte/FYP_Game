using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Go_To_Battle_Interaction : MonoBehaviour, IInteractable
{
    public string InteractionPrompt { get; }
    public bool Interact(Interactor interactor)
    { 
        SceneManager.LoadScene("Battle"); 
        return true;
    }
}
