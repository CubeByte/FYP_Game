using UnityEngine;
using Object = System.Object;

public class TestInteraction : MonoBehaviour,IInteractable
{
    public string InteractionPrompt { get; }
    public bool Interact(Interactor interactor)
    {
        Destroy(this.gameObject);
        
        Debug.Log("interacted with " + this.name);
        
        return true;
    }
}
