using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerRB;
    private PlayerBulletObjPool playbullets;

    private float PosX;
    private float PosZ;
    private int Life;

    private bool isRevive;

    private GameObject playermodeText;
    private float RestartTime;
    private const int Speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        RestartTime = 5.0f;
        isRevive = true;
        Life = 3;
        PlayerPrefs.SetInt("score", 0);
        GFunc.SendTxt("highScoreTxt", $"HighScore : {PlayerPrefs.GetInt("highscore")}");
        GFunc.SendTxt("nowScoreTxt", $"Score : {PlayerPrefs.GetInt("score")}");

        playerRB = gameObject.GetComponent<Rigidbody>();
        playbullets = GFunc.GetRootObj("bulletManager").GetComponent<PlayerBulletObjPool>();
        playermodeText = GFunc.GetRootObj("UiObj").FindChild("playerMode");

        PosX = 0;
        PosZ = 0;
        Revive();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRevive == false)
        {
            PlayerKey();
        }
        else if (isRevive == true)
        {
            if (transform.position.z > 1.0f)
            {
                RestartTime = 5.0f;
                playerRB.velocity = Vector3.zero;
                isRevive = false;
            }
        }
        else { /*Do nothing*/ }

        if (RestartTime >= 0.0f)
        {
            playermodeText.SetActive(true);
            RestartTime -= Time.deltaTime;
            GFunc.SendTxt("playerMode", $"Invincible time : {(int)RestartTime}");
        }
        else
        {
            playermodeText.SetActive(false);
        }
    }

    private void PlayerKey()
    {
        PosX = Input.GetAxis("Horizontal");
        PosZ = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playbullets.BulletFire();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerPrefs.SetInt("highscore", 0);
            PlayerPrefs.SetInt("score", 0);
            GFunc.SendTxt("highScoreTxt", $"HighScore : {PlayerPrefs.GetInt("highscore")}");
            GFunc.SendTxt("nowScoreTxt", $"Score : {PlayerPrefs.GetInt("score")}");
        }

        playerRB.velocity = new Vector3(PosX * Speed, 0, PosZ * Speed);


        //플레이어 위치 조정 x
        if (transform.position.x <= -32)
        {
            transform.position = new Vector3(-31.9f, 0, transform.position.z);
        }
        else if (transform.position.x >= 32)
        {
            transform.position = new Vector3(31.9f, 0, transform.position.z);
        }

        //플레이어 위치 조정 z
        if (transform.position.z <= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0.1f);
        }
        else if (transform.position.z >= 30)
        {
            transform.position = new Vector3(transform.position.x, 0, 29.9f);
        }
    }

    private void playerDestory()
    {
        playerRB.velocity = Vector3.zero;
        transform.position = new Vector3(0, 0, -5);
        isRevive = true;
        gameObject.SetActive(false);

        if (Life > 1)
        {
            Revive();
        }
        else
        {
            PlayerPrefs.SetInt("gameOver", 1);
        }
    }

    private void Revive()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(0, 0, -5);
        playerRB.AddForce(new Vector3(0, 0, 10), ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isRevive == false && RestartTime < 0.0f)
        {
            if (other.tag.Equals("Enemy"))
            {
                playerDestory();
                Life -= 1;
                DeleteLife(Life);
            }
            else if (other.tag.Equals("EnemyBullet"))
            {
                playerDestory();
                Life -= 1;
                DeleteLife(Life);
            }
        }
    }

    private void DeleteLife(int life_)
    {
        GameObject temp;
        if (Life == 2)
        {
            temp = GFunc.GetRootObj("GameObjectClones").FindChild("PlayerDummi2");
            temp.SetActive(false);
        }
        else if (Life == 1)
        {
            temp = GFunc.GetRootObj("GameObjectClones").FindChild("PlayerDummi1");
            temp.SetActive(false);
        }
    }
}
