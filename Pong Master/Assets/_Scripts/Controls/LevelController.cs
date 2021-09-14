using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [HideInInspector] public static int taskNumber = 1;
    [HideInInspector] public static int taskComplete = 0;
    [HideInInspector] public static int level = 1;
    [HideInInspector] public static LevelContent levelContent;

    private void Start()
    {
        LoadLevel(1);
        GameMaster.ShotBall += AddNewBall;
    }
    private void OnDestroy()
    {
        GameMaster.ShotBall -= AddNewBall;
    }
    private GameObject contentPrefab;
    public void LoadLevel(int i)
    {
        //Chose level to load
        level = i;

        //Create levelPlay and it's content data
        contentPrefab = (GameObject) Resources.Load("Level" + i.ToString(), typeof(GameObject));
        Instantiate(contentPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        levelContent = contentPrefab.GetComponent<LevelContent>();

        //Tasks setup
        taskComplete = 0;
        taskNumber = levelContent.numberOfTasks;

        //Add new ball
        AddNewBall();

        //Load Level
        GameMaster.LoadLevel?.Invoke();
    }

    public void AddNewBall()
    {
        StartCoroutine(CreateBall(0.5f));
    }

    public IEnumerator CreateBall(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PoolingSystem.instance.GiveBall(contentPrefab.transform.GetChild(0).position);
    }
}
