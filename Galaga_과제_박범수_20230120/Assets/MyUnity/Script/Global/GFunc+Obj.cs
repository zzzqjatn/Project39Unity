using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public static partial class GFunc
{
    public static GameObject FindChild(this GameObject TargetObj_, string ObjName_)
    {
        GameObject search = default;
        GameObject target = default;

        for (int i = 0; i < TargetObj_.transform.childCount; i++)
        {
            search = TargetObj_.transform.GetChild(i).gameObject;
            if (search.name.Equals(ObjName_))
            {
                target = search;
                return target;
            }
            else
            {
                target = FindChild(search, ObjName_);
            }
        }
        return target;
    }

    public static GameObject GetRootObj(string ObjName_)
    {
        Scene ActiveScene = GetActiveScene();
        GameObject[] RootObj_ = ActiveScene.GetRootGameObjects();

        GameObject ResultObj_ = default;
        foreach (GameObject Obj_ in RootObj_)
        {
            if (Obj_.name.Equals(ObjName_))
            {
                return Obj_;
            }
            else { continue; }
        }
        return ResultObj_;
    }

    public static Scene GetActiveScene()
    {
        Scene tempScene = SceneManager.GetActiveScene();
        return tempScene;
    }

    public static void loadscene(string SceneName_)
    {
        SceneManager.LoadScene(SceneName_);
    }
}
