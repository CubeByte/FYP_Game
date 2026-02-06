using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Scriptable Objects/MapData")]
public class MapData : ScriptableObject
{
    private const string SAVE_KEY = "CurrentEncounter";
    public int currentEncounter;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            currentEncounter = PlayerPrefs.GetInt(SAVE_KEY);
            return;
        }
        currentEncounter = 0;
    }

    public void IncrementEncounter()
    {
        currentEncounter++;
        PlayerPrefs.SetInt(SAVE_KEY, currentEncounter);
        PlayerPrefs.Save();
    }

    public void ResetEncounter()
    {
        currentEncounter = 0;
        PlayerPrefs.SetInt(SAVE_KEY, currentEncounter);
        PlayerPrefs.Save();
    }

    public void SavePlayerEncounter()
    {
        PlayerPrefs.SetInt(SAVE_KEY, currentEncounter);
        PlayerPrefs.Save();
    }
}
