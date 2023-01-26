using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private const float PLAYER_STEP_ON_Y_ANGLE_MIN = 0.7f;  //! <45도 각도
    public AudioClip deathSound = default;
    public float jumpForce = default;
    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;


    #region player's Component
    private Rigidbody2D playerRB;
    private Animator playrAni;
    private AudioSource playerAudio;
    #endregion

    void Start()
    {
        playerRB = gameObject.GetComponentMust<Rigidbody2D>();
        playrAni = gameObject.GetComponentMust<Animator>();
        playerAudio = gameObject.GetComponentMust<AudioSource>();

        // //랩핑, 랩퍼 (비닐로 감싸듯이)
        // // Assert : 조건을 만족하지 않으면(false일때) 에러를 낸다. 체크, 확인용
        // GFunc.Assert(playerRigidbody != null || playerRigidbody != default);
        // GFunc.Assert(animator != null || animator != default);
        // GFunc.Assert(playerAudio != null || playerAudio != default);
    }

    void Update()
    {
        //Return If player dead
        if (isDead == true) { return; }

        // { 플레이어 점프 관련 로직
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            //점프키 누르는 순간 움직임 완전히 멈춤
            playerRB.velocity = Vector2.zero;

            playerRB.AddForce(new Vector2(0, jumpForce));

            playerAudio.Play();

            //애니메이션 시작하는 메서드 (플레이할 애니메이션 명, -1 : 처음부분, 시작시간)
            playrAni.Play("ToKoJumpAni", -1, 0f);

        }   //if: 플레이어가 점프 할 때
        else if (Input.GetMouseButtonUp(0) && 0 < playerRB.velocity.y)
        {
            //나눗셈보다 곱셈이 더 빠르다.
            playerRB.velocity = playerRB.velocity * 0.5f;
        }   //else if: 플레이어가 공중에 떠있을 때

        playrAni.SetBool("Grounded", isGrounded);
        // } 플레이어 점프 관련 로직

        // 점프 중이 아닐 때 그라운드에서 사용하는 로직

    }   //Update()

    //! player die
    private void Die()
    {
        playrAni.SetTrigger("Die");

        playerAudio.clip = deathSound;
        playerAudio.Play();

        playerRB.velocity = Vector2.zero;
        isDead = true;
    }

    //! Tigger 충돌 감지 처리를 위한 함수
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("DeadZone") && isDead == false)
        {
            Die();
        }
    }

    //! 바닥에 닿았는지 체크하는 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PLAYER_STEP_ON_Y_ANGLE_MIN < collision.contacts[0].normal.y)
        {
            isGrounded = true;
            jumpCount = 0;
        }   //45도 보다 완만한 땅을 밟은 경우
    }

    //! 바닥에서 벗어났는지 체크하는 함수
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
