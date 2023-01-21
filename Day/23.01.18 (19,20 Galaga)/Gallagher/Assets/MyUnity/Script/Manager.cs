using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private float ManagerTime;
    private const int STAGE_CLEAR_POINT = 160;
    GameObject stageTxt;
    GameObject EndText;
    void Start()
    {
        ManagerTime = 0.0f;
        PlayerPrefs.SetInt("stage", 1);
        PlayerPrefs.SetInt("gameOver", 0);
        stageTxt = GFunc.GetRootObj("UiObj").FindChild("gameStartTxt");
        EndText = GFunc.GetRootObj("UiObj").FindChild("gameOverTxt");
        stageTxt.SetActive(true);
        GFunc.SendTxt("gameStartTxt", $"STAGE : {PlayerPrefs.GetInt("stage")}");

        PlayerPrefs.SetInt("stageClear", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("gameOver") == 0)
        {
            //잠시 스테이지 클리어타임
            if (PlayerPrefs.GetInt("stageClear") == 1)
            {
                ManagerTime += Time.deltaTime;

                startStage();
            }
            else if (PlayerPrefs.GetInt("stageClear") == 0)
            {
                //인게임중
                if (PlayerPrefs.GetInt("score") != 0 &&
                PlayerPrefs.GetInt("score") % (STAGE_CLEAR_POINT * (PlayerPrefs.GetInt("stage") - 1)) == 0)
                {
                    nextStage();
                }
            }
        }
        else if (PlayerPrefs.GetInt("gameOver") == 1)
        {
            PlayerPrefs.SetInt("stageClear", 0);
            EndText.SetActive(true);
            GFunc.SendTxt("EndHighTxt", $"HighScore : {PlayerPrefs.GetInt("highscore")}");

            if (Input.GetKeyDown(KeyCode.R))
            {
                GFunc.loadscene("GameScene");
            }
        }
    }
    private void startStage()
    {
        if (ManagerTime > 2.0f)
        {
            stageTxt.SetActive(false);
            int temp = PlayerPrefs.GetInt("stage");
            PlayerPrefs.SetInt("stage", temp + 1);
            PlayerPrefs.SetInt("stageClear", 0);
        }
    }
    public void nextStage()
    {
        ManagerTime = 0.0f;
        stageTxt.SetActive(true);
        GFunc.SendTxt("gameStartTxt", $"STAGE : {PlayerPrefs.GetInt("stage")}");
        PlayerPrefs.SetInt("stageClear", 1);
    }

}
