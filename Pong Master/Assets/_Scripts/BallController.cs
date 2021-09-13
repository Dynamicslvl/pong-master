using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D body;
    bool shot, pull;
    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        shot = pull = false;
        body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;
    }

    void Update()
    {
        FallOutside();
        if (shot) return;
        if (Input.GetMouseButton(0)) Pulling();
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
            gameObject.SetActive(false);
        }
    }
    void Pulling()
    {
        if (!pull)
        {
            pull = true;
            PredictPathController.instance.gravityScale = 5;
            PredictPathController.instance.basePosition = PredictPathController.instance.mousePosition;
            PredictPathController.instance.ShowPath();
        }
    }

    void Release()
    {
        shot = true;
        body.isKinematic = false;
        body.velocity = (PredictPathController.instance.basePosition - PredictPathController.instance.mousePosition) * 3;
        PredictPathController.instance.HidePath();
    }
}
