using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Encounter : MonoBehaviour, IPointerClickHandler
{
    public enum EncounterState
    {
        Locked,
        CanVisit,
        Visited
    }

    public CharacterSet enemySet;
    private EncounterState state;
    
    public Color lockedColor;
    public Color canVisitColor;
    public Color visitedColor;
    
    public Renderer renderer;

    public void SetState(EncounterState newState)
    {
        state = newState;

        switch (state)
        {
            case EncounterState.Locked:
            {
                renderer.material.color = lockedColor;
                break;
            }
            case EncounterState.CanVisit:
            {
                renderer.material.color = canVisitColor;
                break;
            }
            case EncounterState.Visited:
            {
                renderer.material.color = visitedColor;
                break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (state == EncounterState.CanVisit)
        {
            MapManager.instance.MoveParty(this);
        }
    }
}
