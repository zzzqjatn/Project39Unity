using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletObjPool : MonoBehaviour
{
    public GameObject bulletPrefabs;
    private GameObject player_;
    private List<GameObject> bulletPool;

    void Start()
    {
        //인스턴스화한 clone 위치 변경을 위한 것
        Transform PoolParent = GFunc.GetRootObj("GameObjectClones").transform;

        player_ = GFunc.GetRootObj("Player");
        bulletPool = new List<GameObject>();

        //총알 50발 생성
        GameObject temp;
        for (int i = 0; i < 150; i++)
        {
            temp = Instantiate(bulletPrefabs);
            temp.transform.position = new Vector3(-5, -5, -5);
            temp.transform.SetParent(PoolParent);
            bulletPool.Add(temp);
        }
    }

    void Update()
    {
        ClearStageBullet();
    }

    public void BulletFire(Vector3 MonsPos, int speed_)
    {
        player_ = GFunc.GetRootObj("Player");

        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (bulletPool[i].GetComponent<EnemyBullet>().GetisDead())
            {
                Vector3 temp = MonsPos + new Vector3(0, 0, -2f);

                // //방향값 구하기 몬스터 위치에서 플레이어 방향쪽으로
                // Vector3 dir = player_.transform.position - MonsPos; dir.y = 0f;
                // //쿼터니언값 구하기
                // Quaternion rot = Quaternion.LookRotation(dir.normalized);

                bulletPool[i].GetComponent<EnemyBullet>().Revive(temp, speed_, player_.transform.position);
                break;
            }
            else { continue; }
        }
    }

    public void ClearStageBullet()
    {
        if (PlayerPrefs.GetInt("stageClear") == 1)
        {
            //추후에 외벽으로 빠져서 죽으러 가기로 변경
            for (int i = 0; i < bulletPool.Count; i++)
            {
                if (bulletPool[i].activeSelf == true)
                {
                    bulletPool[i].GetComponent<EnemyBullet>().Destory();
                }
            }
        }
    }
}
