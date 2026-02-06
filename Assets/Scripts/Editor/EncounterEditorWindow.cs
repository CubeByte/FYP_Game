using UnityEditor;
using UnityEngine;

public class EncounterEditorWindow : EditorWindow
{
    private MapData mapData;
    private int newProgress;

    [MenuItem("Tools/Encounter Editor")]
    public static void ShowWindow()
    {
        // Show the editor window
        GetWindow<EncounterEditorWindow>("Encounter Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Edit Encounter Progress", EditorStyles.boldLabel);

        // Get the current MapData instance
        mapData = (MapData)EditorGUILayout.ObjectField("MapData", mapData, typeof(MapData), false);

        if (mapData != null)
        {
            // Show current progress
            GUILayout.Label("Current Encounter Progress: " + mapData.currentEncounter);

            // Field for setting new progress
            newProgress = EditorGUILayout.IntField("Set Progress:", newProgress);

            // Button to apply changes
            if (GUILayout.Button("Set Progress"))
            {
                mapData.currentEncounter = newProgress;
                PlayerPrefs.SetInt("CurrentEncounter", newProgress);
                PlayerPrefs.Save();
                EditorUtility.SetDirty(mapData);  // Mark MapData as dirty to save it
                Debug.Log("Progress updated to: " + newProgress);
            }
        }
        else
        {
            GUILayout.Label("Please assign a MapData asset.");
        }
    }
}