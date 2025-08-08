using UnityEditor;
using UnityEngine;

public class ObstacleEditor : EditorWindow
{
    // Private 
    private ObstacleInfo obstacleInfo;
    private int gridLength = 10;
    
    [MenuItem("Window/Obstacle Editor")]
    public static void ShowWindow()
    {
        GetWindow<ObstacleEditor>("Obstacle Editor");
    }

    private void OnGUI()
    {
        if (obstacleInfo == null)
        {
            obstacleInfo = (ObstacleInfo)EditorGUILayout.ObjectField("Obstacle Info", obstacleInfo, typeof(ObstacleInfo), false);
        }
        EditorGUILayout.LabelField("Obstacle Grids", EditorStyles.boldLabel);

        for (int i = 0; i < gridLength; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < gridLength; j++)
            {
                int index = i * gridLength + j;
                obstacleInfo.obstacleTiles[index] = GUILayout.Toggle(obstacleInfo.obstacleTiles[index], "");
            }
            EditorGUILayout.EndHorizontal();
        }

        // Save changes
        if (GUI.changed)
        {
            EditorUtility.SetDirty(obstacleInfo);
        }
    }

}
