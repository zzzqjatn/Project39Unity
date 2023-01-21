using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    float scrollSpeed = -0.05f;

    private Material material_;


    // Start is called before the first frame update
    void Start()
    {
        material_ = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float newOffsetY = material_.mainTextureOffset.y + scrollSpeed * Time.deltaTime;
        Vector2 newOffset = new Vector2(0, newOffsetY);

        material_.mainTextureOffset = newOffset;
    }
}
