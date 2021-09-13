using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    public static PoolingSystem instance;

    public GameObject BallPrefab;
    [HideInInspector] public List<GameObject> Balls = new List<GameObject>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(gameObject);
        }
        for(int i = 0; i<6; i++)
        {
            Balls.Add(Instantiate(BallPrefab, new Vector3(0, 0, 0), Quaternion.identity));
            Balls[i].SetActive(false);
        }
    }

    public void GiveBall(Vector3 position)
    {
        for(int i = 0; i<6; i++)
        {
            if(Balls[i].activeSelf == false)
            {
                Balls[i].SetActive(true);
                Balls[i].GetComponent<BallController>().Reset();
                Balls[i].transform.position = position;
                break;
            }
        }
    }

    public void RecoverBall()
    {
        //The ball just set to not active
    }
}
