using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactRadius = 0.5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private InteractionPromptUI interactPromptUI;
    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numberFound;
    private bool interacted = false;

    private IInteractable interactable;
    private void Update()
    {
        numberFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactRadius, colliders, interactableLayer);

        if (numberFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (!interactPromptUI.isDisplayed) interactPromptUI.SetUp();

                if (interacted) interactable.Interact(this);
                interacted = false;
            }
        }
        else
        {
            if(interactable != null) interactable = null;
            if (interactPromptUI.isDisplayed) interactPromptUI.Close();
            interacted = false;
        }
        interacted = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactRadius);
    }

    public void Interact()
    {
        interacted = true;
    }
}