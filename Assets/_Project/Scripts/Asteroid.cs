using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public int childrensToSpawn;
    private void Update() {
        DetectingScreenEdge();
    }
    void OnDestroy()
    {

    }
    void DetectingScreenEdge()
    {
        if (this.transform.position.x>Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x)
        {
            this.transform.position=new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x,this.transform.position.y);
        }
        if (this.transform.position.x<Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x)
        {
            this.transform.position=new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x,this.transform.position.y);
        }
        if (this.transform.position.y>Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y)
        {
            this.transform.position=new Vector2(this.transform.position.x,Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y);
        }
        if (this.transform.position.y<Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y)
        {
            this.transform.position=new Vector2(this.transform.position.x,Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y);
        }
    }
    private void OnEnable() {
        GetComponent<Rigidbody2D>().AddForce(RandomVelocityDirection().normalized*moveSpeed);
    }
    Vector2 RandomVelocityDirection()
    {
        return new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
    }
    void Explode()
    {
        for (int i=0;i<childrensToSpawn;i++)
        {
            var asteroid=GameManager.Instance.GetAsteroidFromPool();
            asteroid.transform.localScale=this.transform.localScale/2;
            asteroid.transform.position=this.transform.position;
            asteroid.gameObject.SetActive(true);
            if(this.transform.localScale.x>0.6f)
                asteroid.GetComponent<Asteroid>().childrensToSpawn=2;
            else
                asteroid.GetComponent<Asteroid>().childrensToSpawn=0;

        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag(TagManager.PLAYER))
        {
            other.gameObject.GetComponent<PlayerController>().CollideWithAsteroid();
        }
        else if(other.gameObject.CompareTag(TagManager.BULLET))
        {
            Explode();
            AudioManager.Instance.PlaySound2D("Explode");
            this.gameObject.SetActive(false);
            GameManager.Instance.UpdateScore();
        }
    }
}
