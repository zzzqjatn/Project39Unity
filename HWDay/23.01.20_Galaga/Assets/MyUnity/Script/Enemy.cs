using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float FireBeginningTime;
    private float FireEndTime;
    private EnemyBulletObjPool enemyBullet;
    private List<Vector2> MovePatten;

    private float firstDelay;
    private float EndDelay;
    private bool DelayEnd;
    private bool isDead;
    private bool isMove;
    private Vector3 DirV;
    private int MoveCount;
    private int Endnumber;
    public int GetMoveCount() { return MoveCount; }
    public int GetEndNumber() { return Endnumber; }
    // Start is called before the first frame update
    void Start()
    {
        //MovePatten = new List<Vector2>();
        isDead = true;
        FireBeginningTime = 0.0f;
        FireEndTime = Random.Range(1, 3 + 1);
        enemyBullet = GFunc.GetRootObj("bulletManager").GetComponent<EnemyBulletObjPool>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (DelayEnd == false)
        {
            firstDelay += Time.deltaTime;
            if (EndDelay < firstDelay) DelayEnd = true;
        }
        else if (DelayEnd == true)
        {
            if (isDead == false)
            {
                FireBeginningTime += Time.deltaTime;
                //enemyBullet 총알 발사 허가
                Fire();
                move();
                StageNonDestory();
            }
        }
    }

    private void move()
    {
        //리스트로 준 지점들을 하나씩 가져와 MOVE
        if (MoveCount > 0)
        {
            if (isMove == false)
            {
                DirV = new Vector3(MovePatten[MoveCount - 1].x, 0f, MovePatten[MoveCount - 1].y);
                Vector3 dir = DirV - transform.position;

                if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(dir)) <= 1.0f)
                {
                    isMove = true;
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 12);
                }
            }
            else if (isMove == true)
            {
                if (Vector3.Distance(transform.position, DirV) <= 2.5f)
                {
                    transform.position = new Vector3(MovePatten[MoveCount - 1].x, 0f, MovePatten[MoveCount - 1].y);
                    MoveCount--;
                    isMove = false;
                }
                else
                {
                    transform.position += transform.forward * 12 * Time.deltaTime;
                }
            }
        }
        else if (MoveCount == 0)
        {
            DirV = new Vector3(transform.position.x, 0f, 0f);
            Vector3 dir = DirV - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 6);

            if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(dir)) <= 1.0f)
            {
                MoveCount -= 1;
            }
        }
    }

    public void RespawnEnemy(Vector2 position_, List<Vector2> movePatten_, float delay_, int number_)
    {
        gameObject.SetActive(true);
        isDead = false;
        FireBeginningTime = 0.0f;
        FireEndTime = Random.Range(1, 3 + 1);
        gameObject.transform.position = new Vector3(position_.x, 0f, position_.y);
        firstDelay = 0.0f;
        EndDelay = delay_;
        MovePatten = movePatten_;
        MoveCount = MovePatten.Count;
        Endnumber = number_;
        isMove = false;
        DelayEnd = false;
    }

    public void RespawnEnemy(Vector3 position_)
    {
        gameObject.SetActive(true);
        isDead = false;
        FireBeginningTime = 0.0f;
        FireEndTime = Random.Range(1, 3 + 1);
        gameObject.transform.position = position_;
        //어디에 생성
    }

    private void Fire()
    {
        if (FireBeginningTime >= FireEndTime)
        {
            int RandomSpeed = Random.Range(1, 100 + 1);

            //플레이어를 향해 발사
            if (RandomSpeed > 80)
            {
                enemyBullet.BulletFire(transform.position, 18);
            }
            else
            {
                enemyBullet.BulletFire(transform.position, 8);
            }

            FireBeginningTime = 0.0f;
            FireEndTime = Random.Range(5, 8 + 1);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerBullet")
        {
            Destory();
        }
        else if (col.tag == "Player")
        {
            Destory();
        }
    }

    public void Destory()
    {
        int tempScore = PlayerPrefs.GetInt("score");
        PlayerPrefs.SetInt("score", tempScore + 20);

        if (PlayerPrefs.GetInt("highscore") < tempScore + 20)
        {
            PlayerPrefs.SetInt("highscore", tempScore + 20);
        }

        GFunc.SendTxt("highScoreTxt", $"HighScore : {PlayerPrefs.GetInt("highscore")}");
        GFunc.SendTxt("nowScoreTxt", $"Score : {PlayerPrefs.GetInt("score")}");

        isDead = true;
        gameObject.transform.position = new Vector3(-5, -5, -5);
        gameObject.SetActive(false);
    }

    public void StageNonDestory()
    {
        if (transform.position.x <= -33.0f)
        {
            isDead = true;
            gameObject.transform.position = new Vector3(-5, -5, -5);
            gameObject.SetActive(false);
        }
        else if (transform.position.x > 33.0f)
        {
            isDead = true;
            gameObject.transform.position = new Vector3(-5, -5, -5);
            gameObject.SetActive(false);
        }
        else if (transform.position.z < -3.0f)
        {
            isDead = true;
            gameObject.transform.position = new Vector3(-5, -5, -5);
            gameObject.SetActive(false);
        }
    }

    public void StageClearDestory()
    {
        isDead = true;
        gameObject.transform.position = new Vector3(-5, -5, -5);
        gameObject.SetActive(false);
    }
}
