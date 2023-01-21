using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundPlanet : MonoBehaviour
{
    private bool isclick;
    public Transform target;
    public int speed;
    public Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        isclick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isclick)
        {
            transform.RotateAround(target.position, dir, speed * Time.deltaTime);
        }
    }
    public void ClickSign()
    {
        if (isclick) isclick = false;
        else if (!isclick) isclick = true;
    }
}
