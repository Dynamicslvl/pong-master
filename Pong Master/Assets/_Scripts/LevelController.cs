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
    private bool isHaveBall= false;
    private void Update()
    {
        //Updating Task
        task.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = taskComplete.ToString() + "/" + taskNumber.ToString();
        //Spawn ball
        if (!isHaveBall)
        {
            isHaveBall = true;
            PoolingSystem.instance.GiveBall(ballSpawnPosition);
        }
    }
    public void LoadLevel(int i)
    {
        taskComplete = 0;
        gameContent = (GameObject) Resources.Load("Level" + i.ToString(), typeof(GameObject));
        Instantiate(gameContent, new Vector3(0, 0, 0), Quaternion.identity);
        LevelContent levelContent = gameContent.GetComponent<LevelContent>();
        gameMode.text = System.Enum.GetName(typeof(GameMode), levelContent.gameMode).Replace("_", " ");
        levelDifficulty.text = System.Enum.GetName(typeof(LevelDifficulty), levelContent.levelDifficulty);
        levelText.text = "LEVEL " + i.ToString();
        for(int j = 0; j<6; j++)
        {
            if(j < levelContent.numberOfBalls)ballsRemain.transform.GetChild(j).gameObject.SetActive(true);
            else ballsRemain.transform.GetChild(j).gameObject.SetActive(false);
        }
        task.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = gameModeIcon[(int) levelContent.gameMode];
        taskNumber = levelContent.numberOfTasks;
        ballSpawnPosition = levelContent.transform.GetChild(0).position;
    }
}
