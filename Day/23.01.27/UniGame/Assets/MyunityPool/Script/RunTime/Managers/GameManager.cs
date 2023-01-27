using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = default;

    private const string UI_OBJS = "UIObjs";
    private const string SCORE_TEXT_OBJ = "ScoreText";
    private const string GAME_OVER_UI_OBJ = "GameOver";

    public bool isGameOver = false;

    private GameObject scoreTextObj = default;
    private GameObject gameOverUI = default;
    private int score = default;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // Init
            isGameOver = false;
            GameObject uiObjs_ = GFunc.GetRootObj(UI_OBJS);
            scoreTextObj = uiObjs_.FindChildObj(SCORE_TEXT_OBJ);
            gameOverUI = uiObjs_.FindChildObj(GAME_OVER_UI_OBJ);

            score = 0;
        }   //if: 게임 매니저가 존재하지 않는 경우 변수의 할당 및 초기화
        else
        {
            GFunc.LogWarning("[System] GameManager: Duplicated Object Warning");
            Destroy(gameObject);
        }
    }   //Awake()

    void Start()
    {

    }

    void Update()
    {
        if (isGameOver == true && Input.GetMouseButtonDown(0))
        {
            GFunc.LoadScene(GFunc.GetActiveScene().name);
        }
    }

    //! 점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        if (isGameOver == true) { return; }

        //게임 진행중이라면
        score += newScore;
        scoreTextObj.setTmpText($"Score : {score}");
    }   //AddScore()

    //! 플레이어 사망 시 게임오버를 출력하는 메서드
    public void OnPlayerDead()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
    }   //OnPlayerDead()
}
