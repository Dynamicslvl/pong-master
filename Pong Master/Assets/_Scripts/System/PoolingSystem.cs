using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    public static PoolingSystem instance;
    public GameObject ballPrefab, praise;
    public ParticleSystem effectCollidePrefab, effectStarPrefab;
    [HideInInspector] public List<GameObject> ballPool = new List<GameObject>();
    [HideInInspector] public List<GameObject> praisePool = new List<GameObject>();
    [HideInInspector] public List<ParticleSystem> eCollidePool = new List<ParticleSystem>();
    [HideInInspector] public List<ParticleSystem> eStarPool = new List<ParticleSystem>();

    #region SINGLETON
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            GameMaster.LoadLevel += RecoverPool;
        } else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    private void OnDestroy()
    {
        GameMaster.LoadLevel -= RecoverPool;
    }
    private int iCollide = 0;
    public ParticleSystem GiveEffectCollide(Vector3 position)
    {
        if (iCollide == 8) iCollide = 0;
        if (iCollide >= eCollidePool.Count)
        {
            ParticleSystem par = Instantiate(effectCollidePrefab, transform);
            par.gameObject.SetActive(false);
            eCollidePool.Add(par);
        }
        eCollidePool[iCollide].gameObject.transform.position = position;
        return eCollidePool[iCollide++];
    }
    private int iStar = 0;
    public ParticleSystem GiveEffectStar(Vector3 position)
    {
        if (iStar == 8) iCollide = 0;
        if (iStar >= eStarPool.Count)
        {
            ParticleSystem par = Instantiate(effectStarPrefab, transform);
            par.gameObject.SetActive(false);
            eStarPool.Add(par);
        }
        eStarPool[iStar].gameObject.transform.position = position;
        return eStarPool[iStar++];
    }
    private int iBall = 0;
    public GameObject GiveBall(Vector3 position)
    {
        if (iBall >= ballPool.Count)
        {
            GameObject obj = Instantiate(ballPrefab, transform);
            obj.gameObject.SetActive(false);
            ballPool.Add(obj);
        }
        ballPool[iBall].GetComponent<Ball>().Reset();
        ballPool[iBall].transform.position = position;
        ballPool[iBall].SetActive(true);
        return ballPool[iBall++];
    }
    private int iPraise = 0;
    public GameObject GivePraise(Vector3 position)
    {
        if (iPraise >= praisePool.Count)
        {
            GameObject obj = Instantiate(praise, transform);
            obj.gameObject.SetActive(false);
            praisePool.Add(obj);
        }
        praisePool[iPraise].transform.position = position;
        praisePool[iPraise].SetActive(true);
        return praisePool[iPraise++];
    }
    public void RecoverPool()
    {
        iCollide = iBall = iStar = iPraise = 0;
        foreach(GameObject ball in ballPool)
        {
            ball.SetActive(false);
        }
        foreach (GameObject praise in praisePool)
        {
            praise.SetActive(false);
        }
        foreach (ParticleSystem par in eCollidePool)
        {
            par.gameObject.SetActive(false);
        }
        foreach (ParticleSystem par in eStarPool)
        {
            par.gameObject.SetActive(false);
        }
    }
}
