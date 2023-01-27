using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : GComponent
{
    private float width = default;
    public override void Awake()
    {
        base.Awake();
        width = gameObject.GetRectSizeDelta().x;
    }

    public override void Update()
    {
        if (transform.localPosition.x <= -width)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        Vector3 offset = new Vector3(width * 2f, 0f, 0f);
        transform.localPosition = transform.localPosition + offset;
    }   //Reposition()
}
