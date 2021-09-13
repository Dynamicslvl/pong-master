using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContent : MonoBehaviour
{
    public GameMode gameMode;
    [Range(3, 6)] public int numberOfBalls;
    public LevelDifficulty levelDifficulty;
    [Range(1, 6)] public int numberOfTasks;
}
