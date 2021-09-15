using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [HideInInspector] public static int taskNumber, taskComplete;
    [HideInInspector] public static int ballLeft;
    [HideInInspector] public static int level = 1;
    [HideInInspector] public static LevelState levelState;
    [HideInInspector] public static LevelContent levelContent;

    private void Start()
    {
        LoadLevel(1);
        GameMaster.ShotBall += AddNewBall;
        GameMaster.RestartLevel += ReloadLevel;
        GameMaster.SkipLevel += NextLevel;
        GameMaster.Win += LevelEnd;
        GameMaster.Lose += LevelEnd;
    }
    private void OnDestroy()
    {
        GameMaster.ShotBall -= AddNewBall;
        GameMaster.RestartLevel -= ReloadLevel;
        GameMaster.SkipLevel -= NextLevel;
        GameMaster.Win -= LevelEnd;
        GameMaster.Lose -= LevelEnd;
    }
    private GameObject contentPrefab;
    private void Update()
    {
        //WIN CONDITIONS
        if(taskComplete == taskNumber && levelState != LevelState.Win)
        {
            levelState = LevelState.Win;
            this.Wait(1f, () =>
            {
                //Debug.Log("Level Clear!");
                GameMaster.Win?.Invoke();
            });
        }

        //LOSE CONDITIONS
        if(ballLeft == 0 && levelState == LevelState.Playing)
        {
            LevelController.levelState = LevelState.Lose;
            this.Wait(5f, () =>
            {
                if (LevelController.levelState == LevelState.Lose)
                {
                    //Debug.Log("Level Failed!");
                    GameMaster.Lose?.Invoke();
                }
            });
        }
    }
    public void LevelEnd()
    {
        StopAllCoroutines();
        PoolingSystem.instance.RecoverBall();
        if (contentPrefab != null) Destroy(contentPrefab);
    }
    public void ReloadLevel()
    {
        StopAllCoroutines();
        LoadLevel(level);
    }
    public void NextLevel()
    {
        StopAllCoroutines();
        if (level < 2)
            LoadLevel(level + 1);
        else GameMaster.RestartLevel?.Invoke();
    }
    public void LoadLevel(int i)
    {
        //State of level
        levelState = LevelState.Playing;

        //Choose a level to load
        level = i;

        //If exist contentPrefab, destroy it
        if (contentPrefab != null) Destroy(contentPrefab);

        //Create levelPlay and its content data
        contentPrefab = (GameObject) Resources.Load("Level" + i.ToString(), typeof(GameObject));
        contentPrefab = Instantiate(contentPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        levelContent = contentPrefab.GetComponent<LevelContent>();

        //Tasks setup
        taskComplete = 0;
        taskNumber = levelContent.numberOfTasks;

        //Ball left
        ballLeft = levelContent.numberOfBalls;

        //Add new ball immediately
        StartCoroutine(CreateBall(0f));

        //Load Level
        GameMaster.LoadLevel?.Invoke();
    }

    public void AddNewBall()
    {
        if (ballLeft != 0)
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
