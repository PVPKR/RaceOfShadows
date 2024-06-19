using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Manager : MonoBehaviour
{
    //=>시간이 지나면 자동으로 씬을 넘긴다
    //시간을 받는 변수
    //시간을 받는 변수
    //원하는 시간
    //씬을 넘긴다

    float elapsedTime;

    //객체 변수 초기화, 세팅
    private void Start()
    {
        
    }

    // Update is called once per frame
    //메인 로직
    private void Update() 
    {
        Check_Time();
    }

    void Check_Time()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= 2f) 
        {


            elapsedTime = 0;
            Go_MenuScene();
        }
    }

    public void Go_MenuScene()
    {
        SceneManager.LoadScene("2_Menu_Scene");
    }
}


//public class A
//{
//    //사용하기 위해서는 해당 자료형 타입 변수가 필요 => 생성 및 초기화가 필요
//    public int ab;
//    public void show() { }


//    //정적(static) 타입
//    //객체형 변수 필요x 생성, 초기화
//    public static int cd = 5; // 해당 객체가 만들어지기 전에.. 변수의 영역과 값이 세팅
//    public static void show2(); // 해당 객체가 만들어지기 전에. 함수의 공간과 로직이 세팅
//}

// 씬을 전환을 해야 한다.
// 씬을 제어하는 자료형(클래스 필요) -> 정의되어 있는 영역. // 작업영역(내부에 정의된 객체)
// 로직 수행
// Start is called before the first frame update