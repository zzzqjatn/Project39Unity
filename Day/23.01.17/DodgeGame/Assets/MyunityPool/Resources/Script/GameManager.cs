using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject gameOverText = default;
    public TMP_Text timetxt = default;
    public TMP_Text bestTimeRecordtxt = default;

    private float surviveTime = default;
    private bool isGameOver = false;
    private const string SCENE_NAME = "PlayScene";
    private const string BEST_TIME_RECODE = "BestTime";
    // Start is called before the first frame update
    void Start()
    {
        surviveTime = 0f;
        isGameOver = false;
        gameOverText.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
    }

    // Update is called once per frame
    void Update()
    {
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
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }
        }

        if (!isGameOver)
        {
            surviveTime += Time.deltaTime;  //생존시간 갱신
            timetxt.text = $"Time : {Mathf.FloorToInt(surviveTime)}";
        }

        if (isGameOver)
        {
            //다시시작 버튼 
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SCENE_NAME);
            }
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
        bestTimeRecordtxt.text = $"BestTime : {Mathf.FloorToInt(BestTime)}";
    }
}
