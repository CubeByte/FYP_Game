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
                characters[i].combatActions = prefabCharacter.combatActions != null
                    ? (CombatAction[])prefabCharacter.combatActions.Clone()
                    : new CombatAction[0];
            }
        }
    }
}