using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D body;
    CircleCollider2D coll2D;
    bool shot;
    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        shot = false;
        body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;
        coll2D = GetComponent<CircleCollider2D>();
        coll2D.isTrigger = true;
    }

    void Update()
    {
        FallOutside();
        if (shot) return;
        if (Input.GetMouseButtonDown(0)) Pull();
        if (Input.GetMouseButtonUp(0)) Release();
    }

    void FallOutside()
    {
        if(transform.position.y < -15f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cup"))
        {
            LevelController.taskComplete++;
            Destroy(collision.GetComponent<BoxCollider2D>());
            GameMaster.NewTaskComplete?.Invoke();
            gameObject.SetActive(false);
        }
    }
    void Pull()
    {
        GameMaster.PullBall?.Invoke();
    }

    void Release()
    {
        shot = true;
        body.isKinematic = false;
        coll2D.isTrigger = false;
        body.velocity = (PredictPathController.basePosition - PredictPathController.mousePosition) * 3;
        GameMaster.ShotBall?.Invoke();
    }
}
