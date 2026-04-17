using CharacterData;
using UnityEngine;

public static class PlayerActionUtility
{
    public static bool LearnAction(PlayerPersistantData playerPersistantData, int playerIndex, CombatAction newAction)
    {
        if (playerPersistantData == null)
        {
            Debug.LogWarning("PlayerActionUtility: PlayerPersistantData is missing.");
            return false;
        }

        if (newAction == null)
        {
            Debug.LogWarning("PlayerActionUtility: newAction is null.");
            return false;
        }

        if (playerIndex < 0 || playerIndex >= playerPersistantData.characters.Length)
        {
            Debug.LogWarning("PlayerActionUtility: playerIndex is out of range.");
            return false;
        }

        string label = string.IsNullOrEmpty(newAction.displayName) ? newAction.name : newAction.displayName;

        CombatAction[] current = playerPersistantData.characters[playerIndex].unlockedActions;

        if (current == null)
        {
            playerPersistantData.characters[playerIndex].unlockedActions = new CombatAction[] { newAction };
            Debug.Log($"Added {label} to unlocked actions.");
            return true;
        }

        for (int i = 0; i < current.Length; i++)
        {
            if (current[i] == newAction)
            {
                Debug.Log($"{label} is already unlocked.");
                return false;
            }
        }

        CombatAction[] expanded = new CombatAction[current.Length + 1];

        for (int i = 0; i < current.Length; i++)
        {
            expanded[i] = current[i];
        }

        expanded[current.Length] = newAction;
        playerPersistantData.characters[playerIndex].unlockedActions = expanded;

        Debug.Log($"Added {label} to unlocked actions.");
        return true;
    }

    public static bool EquipAction(PlayerPersistantData playerPersistantData, int playerIndex, int slotIndex, CombatAction actionToEquip)
    {
        if (playerPersistantData == null)
        {
            Debug.LogWarning("PlayerActionUtility: PlayerPersistantData is missing.");
            return false;
        }

        if (actionToEquip == null)
        {
            Debug.LogWarning("PlayerActionUtility: actionToEquip is null.");
            return false;
        }

        if (playerIndex < 0 || playerIndex >= playerPersistantData.characters.Length)
        {
            Debug.LogWarning("PlayerActionUtility: playerIndex is out of range.");
            return false;
        }

        CombatAction[] equipped = playerPersistantData.characters[playerIndex].combatActions;

        if (equipped == null)
        {
            Debug.LogWarning("PlayerActionUtility: combatActions is null.");
            return false;
        }

        if (slotIndex < 0 || slotIndex >= equipped.Length)
        {
            Debug.LogWarning("PlayerActionUtility: slotIndex is out of range.");
            return false;
        }

        string label = string.IsNullOrEmpty(actionToEquip.displayName) ? actionToEquip.name : actionToEquip.displayName;

        for (int i = 0; i < equipped.Length; i++)
        {
            if (i != slotIndex && equipped[i] == actionToEquip)
            {
                Debug.Log($"{label} is already equipped in another slot.");
                return false;
            }
        }

        equipped[slotIndex] = actionToEquip;
        Debug.Log($"Equipped {label} in slot {slotIndex + 1}.");
        return true;
    }

    public static bool LearnAndEquipAction(PlayerPersistantData playerPersistantData, int playerIndex, int slotIndex, CombatAction newAction)
    {
        LearnAction(playerPersistantData, playerIndex, newAction);
        return EquipAction(playerPersistantData, playerIndex, slotIndex, newAction);
    }
}