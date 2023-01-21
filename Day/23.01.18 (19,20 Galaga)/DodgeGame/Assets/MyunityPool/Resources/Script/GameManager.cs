using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private GameObject gameOverText = default;
    private GameObject timetxtObj = default;
    private GameObject bestTimeRecordtxtObj = default;

    private float surviveTime = default;
    private bool isGameOver = false;
    private const string SCENE_NAME = "PlayScene";
    private const string BEST_TIME_RECODE = "BestTime";
    // Start is called before the first frame update
    void Start()
    {
        // { 출력할 텍스트를 찾아온다
        GameObject uiObjs_ = GFunc.GetRootObj("UIobjs");
        timetxtObj = uiObjs_.FindChildObj("timetxt");
        gameOverText = uiObjs_.FindChildObj("gameovertxt");
        bestTimeRecordtxtObj = uiObjs_.FindChildObj("recordtxt");
        // } 출력할 텍스트를 찾아온다

        surviveTime = 0f;
        isGameOver = false;
        gameOverText.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);

        // if (GFunc.GetRootObj("UIobjs") == default || GFunc.GetRootObj("UIobjs") == null)
        // {
        //     Debug.Log("못찾았다");
        // }
        // else
        // {
        //     Debug.Log("찾았다");
        // }
        //gameObject.PBS_FUNC()
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            //다시시작 버튼 
            if (Input.GetKeyDown(KeyCode.R))
            {
                GFunc.LoadScene_(SCENE_NAME);
            }
        }

        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // #if UNITY_EDITOR
                //                 UnityEditor.EditorApplication.isPlaying = false;
                // #endif
                // #if UNITY_EDITOR == fasle
                //             Application.Quit();
                // #endif
                GFunc.QuitThisGame();
            }
        }

        if (!isGameOver)
        {
            surviveTime += Time.deltaTime;  //생존시간 갱신
            GFunc.setTmpText(timetxtObj, $"Time : {Mathf.FloorToInt(surviveTime)}");
        }

    }

    public void EndGame()
    {
        isGameOver = true;
        //gameOverText.SetActive(true);
        gameOverText.transform.localScale = Vector3.one;

        float BestTime = PlayerPrefs.GetFloat(BEST_TIME_RECODE);

        if (BestTime < surviveTime)
        {
            BestTime = surviveTime;
            PlayerPrefs.SetFloat(BEST_TIME_RECODE, BestTime);
        }
        GFunc.setTmpText(bestTimeRecordtxtObj, $"BestTime : {Mathf.FloorToInt(BestTime)}");
    }
}
