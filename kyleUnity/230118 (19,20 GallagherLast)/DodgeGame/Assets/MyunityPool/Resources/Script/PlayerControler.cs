using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody playerRgBody = default;
    public float playerSpeed = 8.0f;
    public GameManager gameManagerInGame = default;

    void Start()
    {
        playerRgBody = gameObject.GetComponent<Rigidbody>();

        //거리값 알려주는 함수
        // Vector3 firstPoint = new Vector3(100f, 0f, 0f);
        // Vector3 secondPoint = new Vector3(500f, 0f, 0f);
        // float distance = (secondPoint - firstPoint).magnitude;

        // Debug.Log($"거리값 : {distance}");
    }   //Start()

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * playerSpeed;
        float zSpeed = zInput * playerSpeed;

        Vector3 playerVelo = new Vector3(xSpeed, 0f, zSpeed);

        playerRgBody.velocity = playerVelo;
    }   //Update()

    //이전 움직임을 캐싱해놓은 함수
    private void LegacyMove()
    {
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            playerRgBody.AddForce(new Vector3(0, 0, playerSpeed));
        }

        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            playerRgBody.AddForce(new Vector3(0, 0, -playerSpeed));
        }

        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            playerRgBody.AddForce(new Vector3(-playerSpeed, 0, 0));
        }

        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            playerRgBody.AddForce(new Vector3(playerSpeed, 0, 0));
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);

        gameManagerInGame.EndGame();
    }   //Die()
}
