﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMaster
{
    public static int level;

    public delegate void GameEvent();
    public static GameEvent SelectChapter;
    public static GameEvent LoadLevel;
    public static GameEvent RestartLevel;
    public static GameEvent SoundClick;
    public static GameEvent MusicClick;

    public delegate void LevelEvent();
    public static LevelEvent PullBall;
    public static LevelEvent ShotBall;
    public static LevelEvent NewTaskComplete;
    public static LevelEvent Win;
    public static LevelEvent Lose;
}