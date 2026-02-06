using System.Collections.Generic;
using CharacterData;
using Managers;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
   public MapParty party;
   public List<Encounter> encounters = new List<Encounter>();
   public MapData mapData;

   public static MapManager instance;

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
      UpdateEncounterStates();
      
      party.transform.position = encounters[mapData.currentEncounter].transform.position;
   }

   void UpdateEncounterStates()
   {
      for (int i = 0; i < encounters.Count; i++)
      {
         if (i <= mapData.currentEncounter)
         {
            encounters[i].SetState(Encounter.EncounterState.Visited);
         }
         else if (i == mapData.currentEncounter + 1)
         {
            encounters[i].SetState(Encounter.EncounterState.CanVisit);
         }
         else if (i > mapData.currentEncounter + 1)
         {
            encounters[i].SetState(Encounter.EncounterState.Locked);
         }
      }
   }

   public void MoveParty(Encounter encounter)
   {
      party.moveToEncounter(encounter, OnPartyArrivedAtEncounter);
   }

   void OnPartyArrivedAtEncounter(Encounter encounter)
   {
      //mapData.currentEncounter = encounters.IndexOf(encounter);
      GameManager.currentEnemySet = encounter.enemySet;
      SceneManager.LoadScene("Battle");
   }
   public void LoadMenuScene()
   {
      GameManager.instance.LoadMenuScene();
   }
}
