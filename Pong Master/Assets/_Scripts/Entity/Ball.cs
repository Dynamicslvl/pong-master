using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

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
        body = GetComponent<Rigidbody2D>();
        coll2D = GetComponent<CircleCollider2D>();
        release = hold = false;
        body.isKinematic = true;
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

    public void OnEnable()
    {
        GetComponent<SpriteRenderer>().sprite = GameManager.instance.dataBall.ballTypes[GameManager.ballId].sprite;
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.2f);
        transform.rotation = Quaternion.identity;
    }

    void FallOutside()
    {
        if(transform.position.y < -15f)
        {
            gameObject.SetActive(false);
        }
    }

    private float beforeTimeParticle = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time - beforeTimeParticle > 0.3f)
        {
            if(!collision.collider.CompareTag("Cup")) AudioManager.instance.Play("Pong1");
            else AudioManager.instance.Play("BallCollision");
            ParticleSystem effect = PoolingSystem.instance.GiveEffectCollide(collision.contacts[0].point);
            effect.gameObject.SetActive(true);
            effect.Play();
            beforeTimeParticle = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cup"))
        {
            AudioManager.instance.Play("WaterDrop");
            GameMaster.NewTaskComplete?.Invoke();
            gameObject.SetActive(false);
        }
        if (collision.CompareTag("Star"))
        {
            AudioManager.instance.Play("CollectStar");
            ParticleSystem effect = PoolingSystem.instance.GiveEffectCollide(transform.position);
            effect.gameObject.SetActive(true);
            effect.Play();
            GameMaster.NewTaskComplete?.Invoke();
        }
    }

    public void Release()
    {
        LevelController.ballLeft--;
        release = true;
        body.isKinematic = false;
        coll2D.isTrigger = false;
        body.velocity = (PredictPath.basePosition - PredictPath.mousePosition) * GameManager.Fpush;
        GameMaster.ShotBall?.Invoke();
    }
}
