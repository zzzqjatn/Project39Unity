using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public static partial class GFunc
{

    //! 특정 오브젝트의 자식 오브젝트를 서치해서 찾아주는 재귀함수
    public static GameObject FindChildObj(this GameObject targetObj_, string objName_)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;

        for (int i = 0; i < targetObj_.transform.childCount; i++)
        {
            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            if (searchTarget.name.Equals(objName_))
            {
                searchResult = searchTarget;
                return searchResult;
            }
            else
            {
                searchResult = FindChildObj(searchTarget, objName_);
                //Debug.Log(objName_ + " 못찾았다");
            }
        }   //loop

        return searchResult;
    }       //FindChildObj()

    //! 특정 오브젝트의 자식 오브젝트를 서치해서 찾아주는 함수
    // private static GameObject GetChildObj(this GameObject targetObj_, string objName_)
    // {
    //     GameObject searchResult = default;
    //     for (int i = 0; i < targetObj_.transform.childCount; i++)
    //     {
    //         if (targetObj_.transform.GetChild(i).
    //         gameObject.name.Equals(objName_))
    //         {
    //             return searchResult = targetObj_.transform.GetChild(i).gameObject;
    //         }   //if: 타겟 오브젝트에서 이름이 같은 오브젝트를 찾아서 리턴
    //         else { continue; }
    //     }   //loop

    //     //이름이 같은 오브젝트를 못찾을 경우 default값 리턴
    //     return searchResult;
    // }       //GetChildObj()

    //! 씬의 루트 오브젝트를 서치해서 찾아주는 함수
    public static GameObject GetRootObj(string objName_)
    {
        Scene activeScene_ = GetActiveScene();
        GameObject[] rootObjs_ = activeScene_.GetRootGameObjects();

        GameObject targetObj_ = default;
        foreach (GameObject rootObj in rootObjs_)
        {
            if (rootObj.name.Equals(objName_))
            {
                targetObj_ = rootObj;
                return targetObj_;
            }
            else { continue; }
        }   //loop

        return targetObj_;
    }   //GetRootObj()

    //! 현재 활성화 되어있는 씬을 찾아주는 함수
    public static Scene GetActiveScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        return activeScene;
    }   //GetActiveScene()
}
