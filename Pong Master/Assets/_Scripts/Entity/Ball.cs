using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    Rigidbody2D body;
    CircleCollider2D coll2D;
    bool release, hold;
    void Start()
    {
        Reset();
        
    }

    public void Reset()
    {
        release = hold = false;
        body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;
        coll2D = GetComponent<CircleCollider2D>();
        coll2D.isTrigger = true;
    }
    void Update()
    {
        FallOutside();
        if (release) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                hold = true;
                GameMaster.PullBall?.Invoke();
            }
        }
        if (Input.GetMouseButtonUp(0) && hold) Release();
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
            GameMaster.NewTaskComplete?.Invoke();
            gameObject.SetActive(false);
        }
    }

    void Release()
    {
        LevelController.ballLeft--;
        release = true;
        body.isKinematic = false;
        coll2D.isTrigger = false;
        body.velocity = (PredictPath.basePosition - PredictPath.mousePosition) * 3;
        GameMaster.ShotBall?.Invoke();
    }
}
