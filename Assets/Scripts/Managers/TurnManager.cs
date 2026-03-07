using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    private List<Character> turnOrder = new List<Character>();
    private int currentTurnOrderIndex;
    private Character currentTurnCharacter;

    [Header("Components")] public GameObject endTurnButton;
    
    //Singleton
    public static TurnManager instance;
    
    public event UnityAction OnNewTurn;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Begin()
    {
        GenerateTurnOrder(Character.Team.Player);
        NewTurn(turnOrder[0]);
    }

    void GenerateTurnOrder(Character.Team startingTeam)
    {
        if (startingTeam == Character.Team.Player)
        {
            turnOrder.AddRange(GameManager.instance.playerTeam);
            turnOrder.AddRange(GameManager.instance.enemyTeam);
        }
        else
        {
            turnOrder.AddRange(GameManager.instance.enemyTeam);
            turnOrder.AddRange(GameManager.instance.playerTeam);
        }
    }

    void NewTurn(Character character)
    {
        currentTurnCharacter = character;
        OnNewTurn?.Invoke();
        
        endTurnButton.SetActive(character.team == Character.Team.Player);
    }

    //can be called after button pressed, player did combat action, enemy did combat action
    public void EndTurn()
    {
        currentTurnOrderIndex++;

        if (currentTurnOrderIndex == turnOrder.Count)
        {
            currentTurnOrderIndex = 0;
        }

        while (turnOrder[currentTurnOrderIndex] == null)
        {
            currentTurnOrderIndex++;
            if (currentTurnOrderIndex == turnOrder.Count)
                currentTurnOrderIndex = 0;
        }
        
        NewTurn(turnOrder[currentTurnOrderIndex]);
    }

    public Character GetCurrentTurnCharacter()
    {
        return currentTurnCharacter;
    }
}
