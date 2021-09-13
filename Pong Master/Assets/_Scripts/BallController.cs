using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D body;
    bool shot;
    void Start()
    {
        //Create points
        Points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            Points[i] = Instantiate(PointPrefab, transform.position, Quaternion.identity);
            Points[i].transform.localScale = PointScale(i);
            Points[i].SetActive(false);
        }
        //Body setup
        body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;
    }

    public void Reset()
    {
        shot = false;
        /*
        for (int i = 0; i < numberOfPoints; i++)
        {
            Points[i].SetActive(false);
        }
        */
        body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;
    }

    void Update()
    {
        if (shot) return;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0)) Pulling();
        if (Input.GetMouseButtonUp(0)) Release();
    }

    Vector3 mousePosition;
    Vector3 basePosition;
    bool pull = false;

    public GameObject PointPrefab;
    public GameObject[] Points;
    public int numberOfPoints;

    void Pulling()
    {
        if (!pull)
        {
            pull = true;
            basePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            for (int i = 0; i < numberOfPoints; i++)
            {
                Points[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                Points[i].transform.position = PointPosition((i+1) * 0.05f);
            }
        }
    }

    void Release()
    {
        shot = true;
        body.isKinematic = false;
        body.velocity = (basePosition - mousePosition) * 3;
        for (int i = numberOfPoints - 1; i >= 0; i--)
        {
            Points[i].SetActive(false);
        }
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)transform.position + (Vector2)((basePosition - mousePosition) * 3) * t + 0.5f * (t * t) * Physics2D.gravity * body.gravityScale;
        return position;
    }

    Vector3 PointScale(float i)
    {
        Vector3 scale = new Vector3(0.2f, 0.2f, 1) + ((float)numberOfPoints - i) / (float)numberOfPoints * new Vector3(0.2f, 0.2f, 1);
        return scale;
    }
}
