using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 8.0f;
    private Rigidbody bulletRgBody = default;
    // Start is called before the first frame update
    void Start()
    {
        bulletRgBody = gameObject.GetComponent<Rigidbody>();
        bulletRgBody.velocity = transform.forward * bulletSpeed;

        //총알이 3초뒤 제거되는 코드
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //총알이 무언가와 부딪쳤을 경우 함수
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerControler player = other.GetComponent<PlayerControler>();

            //플레이어의 실제 존재하는 지 확인
            if (player == null || player == default)
            {
                return;
            }
            //플레이어가 있다면
            player.Die();
        }
    }
}
