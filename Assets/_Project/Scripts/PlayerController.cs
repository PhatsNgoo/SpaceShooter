using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rgBody;
    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] ParticleSystem pushFx;
    [SerializeField] public float lives;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxSpeed;

    private void Awake() {
        rgBody=GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }
    private void Update() {
        
        var directionForce=Camera.main.ScreenToWorldPoint(Input.mousePosition)-this.transform.position;
        if(Input.GetKey(KeyCode.W) && GameManager.Instance.g_State==GameState.Playing)
        {
            pushFx.Play();
            rgBody.AddForce(directionForce.normalized*moveSpeed);
        }
        if(Input.GetKey(KeyCode.S) && GameManager.Instance.g_State==GameState.Playing)
        {
            rgBody.AddForce(rgBody.velocity.normalized*-1*moveSpeed);
        }
        if(Input.GetMouseButtonDown(0)&& GameManager.Instance.g_State==GameState.Playing)
        {
            Fire();
        }
        transform.rotation=LookAt2D(directionForce);
        DetectingScreenEdge();
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
    public void CollideWithAsteroid()
    {
        if(lives>0)
        {
            lives--;
            this.transform.position=(Vector2)Camera.main.ViewportToWorldPoint(new Vector2(0.5f,0.5f));
            rgBody.velocity=Vector2.zero;
        }
        else
        {
            UIManager.Instance.EndGame();
            Destroy(this.gameObject,-1);
        }
    }
    void Fire()
    {
        var objectToFire = objectPooler.GetPooledObject(TagManager.BULLET);
        objectToFire.SetActive(true);
        objectToFire.transform.position=bulletSpawnPoint.position;
        objectToFire.transform.right=transform.right;
        objectToFire.GetComponent<Bullet>().Shoot(transform.right);
    }
    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}