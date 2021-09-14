﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [HideInInspector] public static int taskNumber = 1;
    [HideInInspector] public static int taskComplete = 0;
    [HideInInspector] public static int level = 1;
    [HideInInspector] public static LevelState levelState;
    [HideInInspector] public static LevelContent levelContent;

    private void Start()
    {
        LoadLevel(1);
        GameMaster.ShotBall += AddNewBall;
        GameMaster.RestartLevel += ReloadLevel;
    }
    private void OnDestroy()
    {
        GameMaster.ShotBall -= AddNewBall;
        GameMaster.RestartLevel -= ReloadLevel;
    }
    private GameObject contentPrefab;
    private void Update()
    {
        if(levelState == LevelState.Win)
        {
            levelState = LevelState.WaitPrize;
            this.Wait(1.6f, () =>
            {
                Debug.Log("Level Clear!");
                GameMaster.Win?.Invoke();
                if (level < 2)
                    LoadLevel(level + 1);
                else GameMaster.RestartLevel?.Invoke();
            });
        }
    }
    public void ReloadLevel()
    {
        StopAllCoroutines();
        LoadLevel(level);
    }
    public void LoadLevel(int i)
    {
        //State of level
        levelState = LevelState.Playing;

        //Choose a level to load
        level = i;

        //If exist contentPrefab, destroy it
        if (contentPrefab != null) DestroyImmediate(contentPrefab);

        //Create levelPlay and its content data
        contentPrefab = (GameObject) Resources.Load("Level" + i.ToString(), typeof(GameObject));
        contentPrefab = Instantiate(contentPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        levelContent = contentPrefab.GetComponent<LevelContent>();

        //Tasks setup
        taskComplete = 0;
        taskNumber = levelContent.numberOfTasks;

        //Add new ball immediately
        StartCoroutine(CreateBall(0f));

        //Load Level
        GameMaster.LoadLevel?.Invoke();
    }

    public void AddNewBall()
    {
        if (levelState == LevelState.Playing || levelState == LevelState.WaitPrize)
        {
            StartCoroutine(CreateBall(0.5f));
        }
    }

    public IEnumerator CreateBall(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PoolingSystem.instance.GiveBall(contentPrefab.transform.GetChild(0).position);
    }
}
