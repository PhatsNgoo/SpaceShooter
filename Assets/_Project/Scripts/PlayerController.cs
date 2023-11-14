using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rgBody;
    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float lives;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxSpeed;
    [Header("Fire rate per second")] float fireRate;

    private void Awake() {
        rgBody=GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }
    private void Update() {
        
        var directionForce=Camera.main.ScreenToWorldPoint(Input.mousePosition)-this.transform.position;
        if(Input.GetKey(KeyCode.W) )
        {
            rgBody.AddForce(directionForce.normalized*moveSpeed);
        }
        if(Input.GetMouseButtonDown(0))
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
    void Fire()
    {
        Debug.LogError("Fired");
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