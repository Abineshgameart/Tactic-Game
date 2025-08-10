using UnityEditor;
using UnityEngine;

public class ObstacleEditor : EditorWindow
{
    // Private 
    private ObstacleInfo obstacleInfo; // Getting ScriptableObject obstacle Info
    private int gridLength = 10;  // Grid Length
    

    // Obstacle Editior in the top menu underWindows
    [MenuItem("Window/Obstacle Editor")]

    public static void ShowWindow()
    {
        GetWindow<ObstacleEditor>("Obstacle Editor"); // Getting the Obstacle Editor
    }

    // creating the onGUI Elements
    private void OnGUI()
    {
        // If obstalce Info is null
        if (obstacleInfo == null)
        {
            // Informations on Editor
            obstacleInfo = (ObstacleInfo)EditorGUILayout.ObjectField("Obstacle Info", obstacleInfo, typeof(ObstacleInfo), false);
        }
        EditorGUILayout.LabelField("Obstacle Grids", EditorStyles.boldLabel);  // Label or Title

        // Making Check box in a grid view
        for (int i = 0; i < gridLength; i++)
        {
            EditorGUILayout.BeginHorizontal();  // Begin in a Horizontal
            for (int j = 0; j < gridLength; j++)
            {
                int index = i * gridLength + j; // Index
                obstacleInfo.obstacleTiles[index] = GUILayout.Toggle(obstacleInfo.obstacleTiles[index], ""); // creating Toggle buttons in Grid View
            }
            EditorGUILayout.EndHorizontal();  // End of Horizontal
        }

        // Save changes
        if (GUI.changed)
        {
            EditorUtility.SetDirty(obstacleInfo);
        }
    }

}
