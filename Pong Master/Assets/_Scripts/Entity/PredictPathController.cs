using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictPathController : MonoBehaviour
{
    public GameObject PointPrefab;
    public int numberOfPoints;
    [HideInInspector] public GameObject[] Points;
    [HideInInspector] public static Vector3 basePosition, mousePosition;
    [HideInInspector] public Vector3 spawnerPosition;
    [HideInInspector] public int gravityScale = 5;
    private void Start()
    {
        GameMaster.LoadLevel += SetSpawnerPositionOnLoadLevel;
        GameMaster.PullBall += ShowPath;
        GameMaster.ShotBall += HidePath;
        GameMaster.RestartLevel += HidePath;
        Points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            Points[i] = Instantiate(PointPrefab, transform.position, Quaternion.identity);
            Points[i].transform.localScale = PointScale(i);
            Points[i].SetActive(false);
        }
    }
    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (int i = 0; i < numberOfPoints; i++)
        {
            Points[i].transform.position = PointPosition((i + 1) * 0.05f);
        }
    }
    public void SetSpawnerPositionOnLoadLevel()
    {
        spawnerPosition = LevelController.levelContent.transform.GetChild(0).position;
    }
    private void OnDestroy()
    {
        GameMaster.LoadLevel -= SetSpawnerPositionOnLoadLevel;
        GameMaster.PullBall -= ShowPath;
        GameMaster.ShotBall -= HidePath;
        GameMaster.RestartLevel -= HidePath;
    }
    public void ShowPath()
    {
        gravityScale = 5;
        basePosition = mousePosition;
        for (int i = 0; i < numberOfPoints; i++)
        {
            Points[i].SetActive(true);
        }
    }
    public void HidePath()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            Points[i].SetActive(false);
        }
    }
    Vector3 PointScale(float i)
    {
        Vector3 scale = new Vector3(0.2f, 0.2f, 1) + ((float)numberOfPoints - i) / (float)numberOfPoints * new Vector3(0.2f, 0.2f, 1);
        return scale;
    }
    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)spawnerPosition + (Vector2)((basePosition - mousePosition) * 3) * t + 0.5f * (t * t) * Physics2D.gravity * gravityScale;
        return position;
    }
}
