using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjPool : MonoBehaviour
{
    public GameObject Enemy1Prefab;
    public GameObject Enemy2Prefab;
    public GameObject Enemy3Prefab;

    private List<GameObject> MonsPool;
    private float resetTime;

    private int numbersPos;

    void Start()
    {
        //인스턴스화한 clone 위치 변경을 위한 것
        Transform PoolParent = GFunc.GetRootObj("EnemyClones").transform;

        resetTime = 0.0f;
        MonsPool = new List<GameObject>();

        int tempRandom;
        GameObject tempEnemy = default;
        for (int i = 0; i < 20; i++)
        {
            tempRandom = Random.Range(1, 3 + 1);
            switch (tempRandom)
            {
                case 1:
                    tempEnemy = Enemy1Prefab;
                    break;
                case 2:
                    tempEnemy = Enemy2Prefab;
                    break;
                case 3:
                    tempEnemy = Enemy3Prefab;
                    break;
            }
            GameObject temp = Instantiate(tempEnemy);
            temp.transform.position = new Vector3(-5, -5, -5);
            temp.transform.SetParent(PoolParent);
            MonsPool.Add(temp);
        }
    }

    void Update()
    {
        Respawnning2();

        ClearDestory();
    }
    private void Respawnning2()
    {
        if (PlayerPrefs.GetInt("stageClear") == 0)
        {
            resetTime += Time.deltaTime;

            if (resetTime > 3.0f && numbersPos >= 0)
            {
                int RandomRespawnNum = Random.Range(1, 3 + 1);

                Vector2 Rtemp = default;
                switch (RandomRespawnNum)
                {
                    case 1:
                        Rtemp = new Vector2(-30.0f, 33.0f);
                        break;
                    case 2:
                        Rtemp = new Vector2(0.0f, 33.0f);
                        break;
                    case 3:
                        Rtemp = new Vector2(30.0f, 33.0f);
                        break;
                }

                RandomRespawnNum = Random.Range(1, 4 + 1);

                int RandomRespon = Random.Range(1, 6 + 1);  //한꺼번에 나올 숫자
                int respondel = 0;

                for (int i = 0; i < MonsPool.Count; i++)
                {
                    if (MonsPool[i].activeSelf == false)
                    {
                        if (respondel > RandomRespon) break;
                        MonsPool[i].GetComponent<Enemy>().RespawnEnemy(Rtemp, Pattens(RandomRespawnNum, numbersPos), respondel, numbersPos);
                        respondel++;
                        numbersPos--;
                    }
                }
                resetTime = -4.0f;
            }
        }
    }

    private List<Vector2> Pattens(int number, int systempnum)
    {
        List<Vector2> temp = new List<Vector2>();

        if (number == 1)    //오른쪽 바닦쓸기
        {
            temp.Add(EndPattens(systempnum));
            temp.Add(new Vector2(-28.0f, 3.0f));    //그대로 왼쪽으로
            temp.Add(new Vector2(28.0f, 3.0f)); //대각선 오른쪽 아래로
            temp.Add(new Vector2(-28.0f, 28.0f));   //왼쪽 상단
        }
        else if (number == 2)   //왼쪽 바닦쓸기
        {
            temp.Add(EndPattens(systempnum));
            temp.Add(new Vector2(28.0f, 3.0f));
            temp.Add(new Vector2(-28.0f, 3.0f));
            temp.Add(new Vector2(28.0f, 28.0f));
        }
        else if (number == 3)   //오른쪽 찍고 back
        {
            temp.Add(EndPattens(systempnum));
            temp.Add(new Vector2(15.0f, 22.0f));
            temp.Add(new Vector2(8.0f, 8.0f));
        }
        else if (number == 4)  //왼쪽 찍고 back
        {
            temp.Add(EndPattens(systempnum));
            temp.Add(new Vector2(-15.0f, 22.0f));
            temp.Add(new Vector2(-8.0f, 8.0f));
        }
        return temp;
    }
    private Vector2 EndPattens(int staynum)
    {
        int stayY = staynum / 10;
        int stayX = staynum % 10;
        Vector2 temp = new Vector2(-23.0f + (5.0f * stayX), 28.0f - (5.0f * stayY));
        return temp;
    }
    private void Respawnning()
    {
        if (PlayerPrefs.GetInt("stageClear") == 0)
        {
            resetTime += Time.deltaTime;

            if (resetTime > 1.0f)
            {
                int RandomRespawnNum = Random.Range(0, 10);

                for (int i = 0; i < MonsPool.Count; i++)
                {
                    if (MonsPool[i].activeSelf == false)
                    {
                        MonsPool[i].GetComponent<Enemy>().RespawnEnemy(new Vector3(RandomRespawnNum + 5, 0, 27));
                        break;
                    }
                }
                resetTime = 0.0f;
            }
        }
    }
    private void ClearDestory()
    {
        if (PlayerPrefs.GetInt("stageClear") == 1)
        {
            numbersPos = 19;
            //추후에 외벽으로 빠져서 죽으러 가기로 변경
            for (int i = 0; i < MonsPool.Count; i++)
            {
                if (MonsPool[i].activeSelf == true)
                {
                    MonsPool[i].GetComponent<Enemy>().StageClearDestory();
                }
            }
        }
    }
}
