using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject explodeFx;
    [SerializeField] float bulletSpeed;
    [SerializeField] float lifeTime;
    float enableTime;
    private void OnEnable()
    {
        enableTime = Time.time;
    }
    private void Update() {
        if(Time.time-enableTime>lifeTime)
        {
            this.gameObject.SetActive(false);
        }
        DetectingScreenEdge();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == TagManager.ASTEROID)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Shoot(Vector2 direction)
    {
        this.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        Debug.LogError(this.GetComponent<Rigidbody2D>().velocity);

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

}
