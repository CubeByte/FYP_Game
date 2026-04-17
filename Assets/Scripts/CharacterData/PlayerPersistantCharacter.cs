using Combat_Action;
using UnityEngine;

[System.Serializable]
public class PlayerPersistantCharacter
{
    public GameObject characterPrefab;
    public int health;
    public bool isDead;
    public CombatAction[] combatActions;
    public CombatAction[] unlockedActions;
}