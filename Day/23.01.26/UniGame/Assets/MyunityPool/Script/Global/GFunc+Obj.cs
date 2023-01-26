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

        if (searchResult == null || searchResult == default) {/* Do nothing */}
        else { return searchResult; }

        return searchResult;
    }       //FindChildObj()

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

    //T : 제네릭이라 하며 아래 내가 원하는 타입으로 변경할수 있다. (맞춰만 주면 아무 단어나 가능하다 T => AniType, G ...)
    public static T GetComponentMust<T>(this GameObject obj)
    {
        T Component_ = obj.GetComponent<T>();
        //as 만약 부모클래스 중 해당 클래스를 상속 받았다면 그 부분만 사용하겠다.
        //만약 Component_ 가 AudioSource Component 라면 결국 Component 의 자식들 중 하나이기때문에
        //Component로 보고 비교 할수 있게 한다.
        //제네릭 default 를 통해 만들 수 있다.
        //bool isComponentValid = Component_.Equals(null) == false;
        //bool isComponentValid = ((Component)(Component_ as Component)).IsValid();
        GFunc.Assert(Component_.IsValid<T>() != false,
        string.Format("{0}에서 {1}을(를) 찾을 수 없습니다.", obj.name, Component_.GetType().Name));

        return Component_;
    }

    //! 트랜스폼을 사용해서 오브젝트를 움직이는 함수
    public static void Translate(this Transform transform_, Vector2 moveVector)
    {
        transform_.Translate(moveVector.x, moveVector.y, 0f);
    }   //Translate()

    //! RectTransform 에서 sizeDelta를 찾아서 리턴하는 함수
    public static Vector2 GetRectSizeDelta(this GameObject obj_)
    {
        return obj_.GetComponentMust<RectTransform>().sizeDelta;
    }   //GetRectSizeDelta()

    //! 오브젝트의 로컬 포지션을 변경하는 함수
    public static void SetLocalPos(this GameObject obj_, float x, float y, float z)
    {
        obj_.transform.localPosition = new Vector3(x, y, z);
    }
}
