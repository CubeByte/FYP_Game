using System;
using System.Collections.Generic;
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

        void Start()
        {
            if (currentEnemySet == null)
            {
                CreateCharacters(playerPersistantData,defaultEnemySet);
            }
            else
            {
                CreateCharacters(playerPersistantData,currentEnemySet);
            }
            TurnManager.instance.Begin();
        }

        void CreateCharacters(PlayerPersistantData playerData, CharacterSet enemyTeamSet)
        {
            playerTeam = new List<Character>();
            enemyTeam = new Character[enemyTeamSet.characters.Length];

            int playerSpawnIndex = 0;

            for (int i = 0; i < playerData.characters.Length; i++)
            {
                if (!playerData.characters[i].isDead)
                {
                    Character character = CreateCharacter(playerData.characters[i].characterPrefab, playerTeamSpawns[playerSpawnIndex]);
                    character.currentHP = playerData.characters[i].health;
                    //playerTeam[i] = character;
                    playerTeam.Add(character);
                    playerSpawnIndex++;
                }
                //else
                //{
                //    playerTeam[i] = null;
                //}
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

        //update player data
        void PayerTeamWins()
        {
            UpdatePlayerPersistantData();
            Invoke(nameof(LoadMapScene), 0.5f);
        }

        void PayerTeamLoss()
        {
            playerPersistantData.ResetCharacters();
            MapManager.instance.mapData.ResetEncounter();
            SceneManager.LoadScene("Menu");
        }

        void UpdatePlayerPersistantData()
        {
            for(int i = 0; i < playerTeam.Count; i++)
            {
                if (playerTeam[i] != null)
                {
                    playerPersistantData.characters[i].health = playerTeam[i].currentHP;
                }
                else
                {
                    playerPersistantData.characters[i].isDead = true;
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
            SceneManager.LoadScene("Menu");
        }
    }
}
