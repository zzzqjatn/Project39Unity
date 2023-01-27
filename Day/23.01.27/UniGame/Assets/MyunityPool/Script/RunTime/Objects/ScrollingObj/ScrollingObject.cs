using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float scrollingSpeed = default;
    void Start()
    {

    }

    void Update()
    {
        if (GameManager.instance.isGameOver == false)
        {
            transform.Translate(Vector2.left * scrollingSpeed * Time.deltaTime);
        }
    }
}
