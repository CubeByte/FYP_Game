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
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].health = characters[i].characterPrefab.GetComponent<Character>().maxHP;
                characters[i].isDead = false;
            }
        }
    }
}
