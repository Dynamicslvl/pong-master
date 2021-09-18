using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    public static PoolingSystem instance;

    public GameObject BallPrefab;
    public List<GameObject> Balls = new List<GameObject>();

    private void Awake()
    {
        #region SINGLETON
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            for (int i = 0; i < 6; i++)
            {
                Balls.Add(Instantiate(BallPrefab, Vector3.zero, Quaternion.identity));
                Balls[i].SetActive(false);
                DontDestroyOnLoad(Balls[i]);
            }
            GameMaster.LoadLevel += RecoverBall;
        } else
        {
            Destroy(gameObject);
        }
        #endregion
    }
    public GameObject GiveBall(Vector3 position)
    {
        for(int i = 0; i<6; i++)
        {
            if(Balls[i].activeSelf == false)
            {
                Balls[i].SetActive(true);
                Balls[i].GetComponent<Ball>().Reset();
                Balls[i].transform.position = position;
                return Balls[i];
            }
        }
        return null;
    }

    private void OnDisable()
    {
        GameMaster.LoadLevel -= RecoverBall;
    }

    public void RecoverBall()
    {
        for(int i = 0; i<6; i++)
        {
            Balls[i].SetActive(false);
        }
    }
}
