using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleInfo", menuName = "Obstacle/ObstacleInfo")]
public class ObstacleInfo : ScriptableObject
{
    public bool[] obstacleTiles = new bool[100]; // Single Dimentional bool array with 100 space
}
