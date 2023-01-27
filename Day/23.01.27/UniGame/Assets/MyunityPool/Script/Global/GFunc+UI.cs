using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static partial class GFunc
{
    //! 텍스트메쉬프로 형태의 컴포넌트와 텍스트를 설정하는 함수
    public static void setTmpText(this GameObject obj_, string text_)
    {
        TMP_Text tmptext = obj_.GetComponent<TMP_Text>();

        if (tmptext == null || tmptext == default)
        {
            return;
        }   //if 컴포넌트가 없을 경우

        //가져올 컴포넌트가 있을 경우
        tmptext.text = text_;
    }   //setTextMeshPro()
}
