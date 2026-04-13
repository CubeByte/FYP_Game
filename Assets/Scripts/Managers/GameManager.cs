using System;
using System.Collections.Generic;
using Combat_Action;
using CharacterData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public List<Character> playerTeam;
        public Character[] enemyTeam;

        private List<Character> allCharacters = new List<Character>();

        [Header("Components")] 
        public Transform[] playerTeamSpawns;
        public Transform[] enemyTeamSpawns;
    
        [Header("Data")]
        public PlayerPersistantData playerPersistantData;
        public CharacterSet defaultEnemySet;
        public static GameManager instance;
        public static CharacterSet currentEnemySet;
        public MapData mapData;
        public Scene currentScene;

        void Awake()
        {
            currentScene = SceneManager.GetActiveScene();
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }

        void Start()
        {
            currentScene = SceneManager.GetActiveScene();
            if (currentEnemySet == null)
            {
                CreateCharacters(playerPersistantData, defaultEnemySet);
            }
            else
            {
                CreateCharacters(playerPersistantData, currentEnemySet);
            }

            TurnManager.instance.Begin();
        }

        void CreateCharacters(PlayerPersistantData playerData, CharacterSet enemyTeamSet)
        {
            playerTeam = new List<Character>();
            enemyTeam = new Character[enemyTeamSet.characters.Length];
            allCharacters.Clear();

            int playerSpawnIndex = 0;

            for (int i = 0; i < playerData.characters.Length; i++)
            {
                if (!playerData.characters[i].isDead)
                {
                    GameObject prefab = playerData.characters[i].characterPrefab;
                    Character character = CreateCharacter(prefab, playerTeamSpawns[playerSpawnIndex]);

                    character.persistentIndex = i;
                    character.currentHP = playerData.characters[i].health;
                    character.combatActions = playerData.characters[i].combatActions != null
                        ? (CombatAction[])playerData.characters[i].combatActions.Clone()
                        : new CombatAction[0];

                    playerTeam.Add(character);
                    playerSpawnIndex++;
                }
            }
            
            for (int i = 0; i < enemyTeamSet.characters.Length; i++)
            {
                Character character = CreateCharacter(enemyTeamSet.characters[i], enemyTeamSpawns[i]);
                enemyTeam[i] = character;
            }
            
            allCharacters.AddRange(playerTeam);
            allCharacters.AddRange(enemyTeam);
        }

        Character CreateCharacter(GameObject characterPrefab, Transform spawnPos)
        {
            GameObject obj = Instantiate(characterPrefab, spawnPos.position, spawnPos.rotation);
            return obj.GetComponent<Character>();
        }

        public void OnCharacterDeath(Character character)
        {
            allCharacters.Remove(character);

            int playersRemaining = 0;
            int enemiesRemaining = 0;

            for (int i = 0; i < allCharacters.Count; i++)
            {
                if (allCharacters[i].team == Character.Team.Player)
                {
                    playersRemaining++;
                }
                else
                {
                    enemiesRemaining++;
                }
            }

            if (enemiesRemaining == 0)
            {
                PayerTeamWins();
            }

            if (playersRemaining == 0)
            {
                PayerTeamLoss();
            }
        }

        void PayerTeamWins()
        {
            UpdatePlayerPersistantData();
            Transition.Instance.LoadSceneWithFade("Exploration_Zone");
        }

        void PayerTeamLoss()
        {
            MarkDeadPlayers();
            playerPersistantData.ResetCharacters();

            if (currentScene.name == "Battle")
            {
                Transition.Instance.LoadSceneWithFade("Exploration_Zone");
            }
            else
            {
                Transition.Instance.LoadSceneWithFade("Game_Over");
            }
        }

        void UpdatePlayerPersistantData()
        {
            for (int i = 0; i < playerPersistantData.characters.Length; i++)
            {
                playerPersistantData.characters[i].isDead = true;
            }

            for (int i = 0; i < playerTeam.Count; i++)
            {
                if (playerTeam[i] != null && playerTeam[i].persistentIndex >= 0)
                {
                    int index = playerTeam[i].persistentIndex;

                    playerPersistantData.characters[index].health = playerTeam[i].currentHP;
                    playerPersistantData.characters[index].isDead = false;
                    playerPersistantData.characters[index].combatActions = playerTeam[i].combatActions != null
                        ? (CombatAction[])playerTeam[i].combatActions.Clone()
                        : new CombatAction[0];
                }
            }
        }

        void MarkDeadPlayers()
        {
            for (int i = 0; i < playerPersistantData.characters.Length; i++)
            {
                playerPersistantData.characters[i].isDead = true;
            }

            for (int i = 0; i < playerTeam.Count; i++)
            {
                if (playerTeam[i] != null && playerTeam[i].persistentIndex >= 0)
                {
                    playerPersistantData.characters[playerTeam[i].persistentIndex].isDead = false;
                }
            }
        }
        
        void LoadMapScene()
        {
            MapManager.instance.mapData.IncrementEncounter();
            SceneManager.LoadScene("Map");
        }

        public void LoadMenuScene()
        {
            playerPersistantData.ResetCharacters();
            MapManager.instance.mapData.ResetEncounter();
            Transition.Instance.LoadSceneWithFade("Menu");
        }
    }
}