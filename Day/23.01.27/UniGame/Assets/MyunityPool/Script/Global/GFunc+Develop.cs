using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GFunc
{
    #region Print log func
    //랩핑, 랩퍼 (비닐로 감싸듯이)
    [System.Diagnostics.Conditional("DEBUG_MODE")] //개발 할때만 확인하기 위해 한다. 출시 하여 배포 프로그램에선 이게 나오지 않는다.
    public static void Log(object message)
    {
#if DEBUG_MODE  //bulidSetting -> playerSetting -> ScriptCompilation 설정
        Debug.Log(message);
#endif  //DEBUG_MODE
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message, UnityEngine.Object context)
    {
#if DEBUG_MODE
        Debug.Log(message, context);
#endif  //DEBUG_MODE
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void LogWarning(object message)
    {
#if DEBUG_MODE
        Debug.LogWarning(message);
#endif  //DEBUG_MODE
    }
    #endregion  //Print log func

    #region Assert for debug
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition)
    {
#if DEBUG_MODE
        // Debug.Assert : 조건을 만족하지 않으면(false일때) 에러를 낸다. 체크, 확인용
        Debug.Assert(condition);
#endif  //DEBUG_MODE
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition, object message)
    {
#if DEBUG_MODE
        Debug.Assert(condition, message);
#endif  //DEBUG_MODE
    }
    #endregion  //Assert for debug

    #region Vaild Func
    public static bool IsValid<T>(this T component_)
    {
        bool isvalid = component_.Equals(null) == false;
        return isvalid;
    }
    #endregion  //Vaild Func
}
