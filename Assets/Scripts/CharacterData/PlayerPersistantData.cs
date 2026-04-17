using System.Collections.Generic;
using Combat_Action;
using UnityEngine;

namespace CharacterData
{
    [CreateAssetMenu(fileName = "Player Persistant Data", menuName = "New Player Persistant Data")]
    public class PlayerPersistantData : ScriptableObject
    {
        public PlayerPersistantCharacter[] characters;

#if UNITY_EDITOR
        void OnValidate()
        {
            ResetCharacters();
        }
#endif

        public void ResetCharacters()
        {
            if (characters == null)
                return;

            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i] == null)
                    continue;

                if (characters[i].characterPrefab == null)
                {
                    Debug.LogWarning($"PlayerPersistantData: characters[{i}] has no characterPrefab assigned.", this);
                    continue;
                }

                Character prefabCharacter = characters[i].characterPrefab.GetComponent<Character>();

                if (prefabCharacter == null)
                {
                    Debug.LogWarning($"PlayerPersistantData: characterPrefab on slot {i} has no Character component.", this);
                    continue;
                }

                characters[i].health = prefabCharacter.maxHP;
                characters[i].isDead = false;

                // Keep the slot layout exactly as the prefab has it
                characters[i].combatActions = prefabCharacter.combatActions != null
                    ? (CombatAction[])prefabCharacter.combatActions.Clone()
                    : new CombatAction[0];

                // Build unlocked actions from real actions only
                characters[i].unlockedActions = BuildUnlockedActions(prefabCharacter.combatActions);
            }
        }

        private CombatAction[] BuildUnlockedActions(CombatAction[] sourceActions)
        {
            if (sourceActions == null || sourceActions.Length == 0)
                return new CombatAction[0];

            List<CombatAction> filteredActions = new List<CombatAction>();

            for (int i = 0; i < sourceActions.Length; i++)
            {
                CombatAction action = sourceActions[i];

                if (action == null)
                    continue;

                if (IsEmptyAction(action))
                    continue;

                if (!filteredActions.Contains(action))
                {
                    filteredActions.Add(action);
                }
            }

            return filteredActions.ToArray();
        }

        private bool IsEmptyAction(CombatAction action)
        {
            if (action == null)
                return true;

            string displayLabel = string.IsNullOrEmpty(action.displayName) ? action.name : action.displayName;

            return displayLabel == "Empty";
        }
    }
}