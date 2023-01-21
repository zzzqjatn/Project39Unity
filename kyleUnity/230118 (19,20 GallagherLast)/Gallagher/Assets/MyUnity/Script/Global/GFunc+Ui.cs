using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static partial class GFunc
{
    public static void SendTxt(string target, string text_)
    {
        GameObject canvers_ = GFunc.GetRootObj("UiObj");

        GameObject temp = default;
        temp = canvers_.FindChild(target);

        if (temp == null || temp == default)
        {
            return;
        }
        else
        {
            temp.GetComponent<TMP_Text>().text = text_;
        }
    }
}
