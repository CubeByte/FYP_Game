using UnityEngine;

//needs to be damage or heal effect
public abstract class Effect : ScriptableObject
{
   public int durationOfTurns;
   
   [Header("Prefabs")]
   public GameObject activePrefab;
   public GameObject tickPrefab;
}
