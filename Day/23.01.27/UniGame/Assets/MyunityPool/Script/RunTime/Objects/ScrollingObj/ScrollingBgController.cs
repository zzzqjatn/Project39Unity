using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBgController : ScrollingObjController
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void InitObjPosition()
    {
        base.InitObjPosition();

        float horizonPos = objPrefabSize.x * (scrollingObjCount - 1) * (-1) * 0.5f;
        for (int i = 0; i < scrollingObjCount; i++)
        {
            scrollingPool[i].SetLocalPos(horizonPos, 0f, 0f);
            horizonPos = horizonPos + objPrefabSize.x;
        }   // loop: 생성한 오브젝트를 가로로 왼쪽부터 차례대로 정렬하는 루프
    }   //InitObjPosition()

    protected override void RepositionFirstObj()
    {
        base.RepositionFirstObj();

        // 스크롤링 풀의 첫번째 오브젝트를 마지막으로 리포지셔닝 하는 로직
        float lastScrObjCurrentXPos = scrollingPool[scrollingObjCount - 1].transform.localPosition.x;
        if (lastScrObjCurrentXPos <= objPrefabSize.x * 0.5f)
        {
            float lastScrObjInitXpos =
                Mathf.Floor(scrollingObjCount * 0.5f) *
                objPrefabSize.x + (objPrefabSize.x * 0.48f);

            scrollingPool[0].SetLocalPos(lastScrObjInitXpos, 0f, 0f);
            scrollingPool.Add(scrollingPool[0]);
            scrollingPool.RemoveAt(0);

            // DEBUG:
            // GFunc.Log($"List Pos : {scrollingPool[0].transform.localPosition}, " +
            // $"{scrollingPool[2].transform.localPosition}");
        }   //if : 스크롤링 오브젝트의 마지막 오브젝트가 화면 상의 절반정도 Draw 되는 때
    }   //RepositionFirstObj()
}
