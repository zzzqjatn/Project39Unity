using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Mono가 붙으면 인스턴스화해서 쓴다는 뜻이다 없으면 안쓴다는 뜻 (유니티 기능을 굳이 상속받을 이유가 없다면)

// public static class GFunc
// {
//     public static void QuitThisGame()
//     {
// #if UNITY_EDITOR
//         UnityEditor.EditorApplication.isPlaying = false;
// #else
//             Application.Quit();
// #endif
//     }
// }

/*(static class) 
static 클래스이면서 굳이 찾을 이유가 없다면 이름과 파일명이 달라도 상관없다 (기능만 쓸거라서, 인스턴스화 하지 안한다)

static 클래스의 용도 : 다양한 using 문들을 추가해주는 수고를 덜고 여기에 using을 하고 메서드를 만들어
GameObject와 매개변수만 받아서 결과만 건네주면 다른 클래스에서 번거로울 필요없이 결과값만 받아 더 깔끔하게 코드가 완성된다. 관리도 편해진다.


static class 의 class와 method 메모리 차지 비율
주소값은 4byte 이기 때문에 (현)

static method 4byte
static method 100개면 400byte
static class 까지 포함 : 400 + 4 byte
*/

/*
(partial class) 
이것을 기능별로 분활하기 위해 static 클래스에 partial을 붙어 다른 위치지만 같은 클래스명으로 동일화되어 사용할 수 있다.
(사용자가 보기 편리해지고 기능별로 분활된 파일화 가능하다)
*/
public static partial class GFunc
{
    public static void QuitThisGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    /*
    확장메서드(Extended method)
    데이터 변수에 .(점) 찍으면 사용자에게 알려주는 기능
    첫번째 매개변수에 this를 붙이면 어떤 데이터타입의 변수에서 이런 기능도 있다 알려주는 것
    이후 다른 고정 키워드? 고정 메서드? 어딘가에서 제공해주는 메서드처럼 사용할 수 있다.
    뒤에 매개변수를 추가해 주면 그 값을 받는 메서드가 된다.
    */
    public static void PBS_FUNC(this GameObject obj_)
    {
        Debug.Log("내가 만든 함수");
    }
    public static void PBS_FUNC(this GameObject obj_, string text_)
    {
        Debug.Log(text_);
    }
    public static void PBS_FUNC(this GameObject obj_, int a, int b)
    {
        Debug.Log(a + b);
    }

    public static void LoadScene_(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
}
