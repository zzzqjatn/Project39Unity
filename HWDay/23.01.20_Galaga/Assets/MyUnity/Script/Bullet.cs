using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRB;
    private bool isDead;
    public bool GetisDead() { return isDead; }
    private const int bulletSpeed = 30;

    //private const float DeadLine = 30.0f;
    //private float StartLine = 0;
    // Start is called before the first frame update
    void Start()
    {
        isDead = true;
        bulletRB = gameObject.GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= 30.0f)
        {
            Destory();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            Destory();
        }
    }

    public void Destory()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
        isDead = true;
        gameObject.SetActive(false);
    }

    public void Revive(Vector3 position_)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = position_;
        bulletRB.velocity = new Vector3(0, 0, bulletSpeed);
        isDead = false;
        //StartLine = position_.z;
    }
}
