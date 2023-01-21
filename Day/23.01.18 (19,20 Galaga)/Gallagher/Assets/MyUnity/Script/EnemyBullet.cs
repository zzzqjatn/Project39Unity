using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody bulletRB;
    private bool isDead;
    public bool GetisDead() { return isDead; }
    private int bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = gameObject.GetComponent<Rigidbody>();
        isDead = true;
        bulletSpeed = 0;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -3.0f)
        {
            Destory();
        }
        else if (transform.position.x < -35.0f)
        {
            Destory();
        }
        else if (transform.position.x > 35.0f)
        {
            Destory();
        }

        // if (isDead == false)
        // {
        //     bulletRB.velocity = new Vector3(bulletSpeed, 0, bulletSpeed);
        // }
    }

    public void Destory()
    {
        gameObject.transform.position = new Vector3(-5, -5, -5);
        isDead = true;
        bulletSpeed = 0;
        gameObject.SetActive(false);
    }

    public void Revive(Vector3 position_, int speed_, Vector3 playerPos)
    {
        gameObject.SetActive(true);
        bulletSpeed = speed_;
        gameObject.transform.position = position_;

        gameObject.transform.LookAt(playerPos);
        bulletRB.velocity = transform.forward * bulletSpeed;
        gameObject.transform.Rotate(new Vector3(90, 0, 0));

        isDead = false;
    }
}
