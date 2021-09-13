using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    public TextMeshProUGUI gameMode;
    public GameObject task;
    public GameObject ballsRemain;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI levelDifficulty;
    public Sprite[] gameModeIcon = new Sprite[3];
    [HideInInspector] public GameObject gameContent;

    public int taskNumber = 1;
    [HideInInspector] public static int taskComplete = 0;

    private void Start()
    {
        LoadLevel(1);
    }
    private Vector3 ballSpawnPosition;
    private GameObject newBall = null;
    private void Update()
    {
        //Updating Task
        task.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = taskComplete.ToString() + "/" + taskNumber.ToString();
        //Spawn ball
        if (newBall == null || newBall.activeSelf == false)
        {
            newBall = PoolingSystem.instance.GiveBall(ballSpawnPosition);
        }
    }
    public void LoadLevel(int i)
    {
        gameContent = (GameObject) Resources.Load("Level" + i.ToString(), typeof(GameObject));
        Instantiate(gameContent, new Vector3(0, 0, 0), Quaternion.identity);
        
        LevelContent levelContent = gameContent.GetComponent<LevelContent>();

        //Reset number of completed tasks;
        taskComplete = 0;

        //Gamemode Text
        gameMode.text = System.Enum.GetName(typeof(GameMode), levelContent.gameMode).Replace("_", " ");
        
        //Level Difficulty
        levelDifficulty.text = System.Enum.GetName(typeof(LevelDifficulty), levelContent.levelDifficulty);
        
        //Level Text
        levelText.text = "LEVEL " + i.ToString();

        //Ball Remain
        for(int j = 0; j<6; j++)
        {
            if(j < levelContent.numberOfBalls)ballsRemain.transform.GetChild(j).gameObject.SetActive(true);
            else ballsRemain.transform.GetChild(j).gameObject.SetActive(false);
        }

        //Gamemode Icon
        task.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = gameModeIcon[(int) levelContent.gameMode];
        
        //Tasks Count
        taskNumber = levelContent.numberOfTasks;

        //Ball Spawn Positon
        ballSpawnPosition = levelContent.transform.GetChild(0).position;
        PredictPathController.instance.spawnerPosition = ballSpawnPosition;
    }
}
