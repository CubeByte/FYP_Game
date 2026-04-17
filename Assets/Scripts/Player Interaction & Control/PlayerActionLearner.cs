using Combat_Action;
using CharacterData;
using UnityEngine;

public class PlayerActionLearner : MonoBehaviour
{
    [SerializeField] private PlayerPersistantData playerPersistantData;

    public void LearnAction(int playerIndex, CombatAction newAction)
    {
        if (playerPersistantData == null || newAction == null)
            return;

        if (playerIndex < 0 || playerIndex >= playerPersistantData.characters.Length)
            return;

        CombatAction[] current = playerPersistantData.characters[playerIndex].unlockedActions;

        if (current == null)
        {
            playerPersistantData.characters[playerIndex].unlockedActions = new CombatAction[] { newAction };
            return;
        }

        for (int i = 0; i < current.Length; i++)
        {
            if (current[i] == newAction)
            {
                return;
            }
        }

        CombatAction[] expanded = new CombatAction[current.Length + 1];

        for (int i = 0; i < current.Length; i++)
        {
            expanded[i] = current[i];
        }

        expanded[current.Length] = newAction;
        playerPersistantData.characters[playerIndex].unlockedActions = expanded;
    }
    public void LearnAndEquipAction(int playerIndex, int slotIndex, CombatAction newAction)
    {
        LearnAction(playerIndex, newAction);

        CombatAction[] equipped = playerPersistantData.characters[playerIndex].combatActions;

        if (equipped != null && slotIndex >= 0 && slotIndex < equipped.Length)
        {
            equipped[slotIndex] = newAction;
        }
    }
}