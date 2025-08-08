using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleInfo", menuName = "Obstacle/ObstacleInfo")]
public class ObstacleInfo : ScriptableObject
{
    public bool[] obstacleTiles = new bool[100];
}
