using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [HideInInspector] public static int taskNumber, taskComplete;
    [HideInInspector] public static int ballLeft;
    [HideInInspector] public static int level = 1;
    [HideInInspector] public static LevelState levelState;
    [HideInInspector] public static LevelContent levelContent;
    private GameObject contentPrefab;

    private void Start()
    {
        MenuController.instance.gameObject.SetActive(false);
        GameMaster.ShotBall += AddNewBall;
        GameMaster.RestartLevel += ReloadLevel;
        GameMaster.SkipLevel += NextLevel;
        GameMaster.Win += LevelEnd;
        GameMaster.Lose += LevelEnd;
        GameMaster.PauseLevel += PauseLevel;
        GameMaster.ResumeLevel += ResumeLevel;
        LoadLevel(1);
    }
    private void OnDestroy()
    {
        GameMaster.ShotBall -= AddNewBall;
        GameMaster.RestartLevel -= ReloadLevel;
        GameMaster.SkipLevel -= NextLevel;
        GameMaster.Win -= LevelEnd;
        GameMaster.Lose -= LevelEnd;
        GameMaster.PauseLevel -= PauseLevel;
        GameMaster.ResumeLevel -= ResumeLevel;
    }
   
    private void Update()
    {
        //WIN CONDITIONS
        if(taskComplete == taskNumber && levelState != LevelState.Win)
        {
            levelState = LevelState.Win;
            StartCoroutine(ActiveLevelWin(1));
        }

        //LOSE CONDITIONS
        if(ballLeft == 0 && levelState == LevelState.Playing)
        {
            LevelController.levelState = LevelState.Lose;
            StartCoroutine(ActiveLevelLose(5));
        }
    }
    IEnumerator ActiveLevelWin(float waitTime)
    {
        while(waitTime > 0)
        {
            if (!isPaused) waitTime -= Time.deltaTime;
            yield return null;
        }
        GameMaster.Win?.Invoke();
    }
    IEnumerator ActiveLevelLose(float waitTime)
    {
        while(waitTime > 0)
        {
            if (!isPaused) waitTime -= Time.deltaTime;
            yield return null;
        }
        if (LevelController.levelState == LevelState.Lose)
        {
            GameMaster.Lose?.Invoke();
        }
    }
    public void LevelEnd()
    {
        StopAllCoroutines();
        if(levelState == LevelState.Win)
        {
            GameManager.levelComplete = Mathf.Max(GameManager.levelComplete, level);
            StringBuilder sb = new StringBuilder(GameManager.starsPerLevel);
            GameManager.starClaim += (Mathf.Min(ballLeft + 1, 3) - (sb[level - 1] - '0'));
            sb[level - 1] = (Mathf.Min(ballLeft + 1, 3)).ToString()[0];
            GameManager.starsPerLevel = sb.ToString();
            GameManager.SaveGame();
        }
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
        {
            LoadLevel(level + 1);
        } else GameMaster.RestartLevel?.Invoke();
    }
    bool isPaused = false;
    public void PauseLevel()
    {
        isPaused = true;
        Time.timeScale = 0;
    }
    public void ResumeLevel()
    {
        isPaused = false;
        Time.timeScale = 1;
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
