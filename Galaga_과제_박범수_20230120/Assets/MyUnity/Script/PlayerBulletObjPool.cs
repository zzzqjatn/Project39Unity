using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletObjPool : MonoBehaviour
{
    public GameObject bulletPrefabs;
    private GameObject player_;

    private List<GameObject> bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        //인스턴스화한 clone 위치 변경을 위한 것
        Transform PoolParent = GFunc.GetRootObj("GameObjectClones").transform;

        player_ = GFunc.GetRootObj("Player");

        bulletPool = new List<GameObject>();

        //총알 20발 생성
        GameObject temp;
        for (int i = 0; i < 20; i++)
        {
            temp = Instantiate(bulletPrefabs);
            temp.transform.SetParent(PoolParent);
            bulletPool.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BulletFire()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (bulletPool[i].GetComponent<Bullet>().GetisDead())
            {
                Vector3 temp = player_.transform.position + new Vector3(0, 0, 2f);
                bulletPool[i].GetComponent<Bullet>().Revive(temp);
                break;
            }
            else { continue; }
        }
    }
}
