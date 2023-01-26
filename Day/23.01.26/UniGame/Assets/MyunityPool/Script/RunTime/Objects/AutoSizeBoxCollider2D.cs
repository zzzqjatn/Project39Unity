using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSizeBoxCollider2D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2 objSize_ = default;
        BoxCollider2D boxcollider_ = gameObject.GetComponentMust<BoxCollider2D>();
        RectTransform parentrectTransform_ = transform.parent.gameObject.GetComponentMust<RectTransform>();
        RectTransform rectTransform_ = gameObject.GetComponentMust<RectTransform>();

        objSize_.x = parentrectTransform_.sizeDelta.x;
        objSize_.y = rectTransform_.sizeDelta.y;

        boxcollider_.size = objSize_;
    }   //Start()
}
