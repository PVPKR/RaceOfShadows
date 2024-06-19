using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Land_Manager : MonoBehaviour
{
    // 코루틴 객체를 만들고 코루틴 객체를 제작 활성화 한다

    //public GameObject Land1; // 프리팹의 정보
    //public GameObject Land2;
    //프리팹 지형 정보를 받을 배열 변수
    public GameObject[] Lands;  // = new GameObject[6];

    // 생성된 프리팹 지형을 받는 변수
    public GameObject tempLand1;
    public GameObject tempLand2;


    public float scroll_gap; // 지형의 크기
    public float map_count; // 맵의 갯수
    //사용자가 정한 지형 형태
    int[] StageArray = new int[] { 0, 0, 0, 0, 3, 1, 5, 2, 3, 4, 2, 5, 3, 3, 4, 2, 2, 3, 1 }; // 지형의 순서를 정하는 배열
    int land_count = 0; // 지형 번호
    void Start()
    {
        

        //for(int i = 0; i<StageArray.Length; i++)
        //{
        //    print("값" + StageArray[i]);
        //}

        tempLand1 = Instantiate(Lands[0]);
        tempLand1.transform.position = new Vector3(0, -2, 0);
        tempLand1.transform.rotation = Quaternion.identity;

        tempLand2 = Instantiate(Lands[0], new Vector3(30, -2, 0), Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
        print("모드 :" + Game_Manager.GameMode);
        Re_Use_Map(); // 지형의 위치 체크하는 함수
    }

    void Re_Use_Map()
    {
        //내 지형의 위치 -30 이하로 내려가면.
        //위치를 전 지형의 초기 위치에 넣는다. (30)
        if (scroll_gap + tempLand1.transform.position.x <= 0f) // 0 30
        {
            Destroy(tempLand1); // 화면을 벗어난 지형 삭제
            //
            tempLand1 = tempLand2; // tempLand2가 접근하는 지형을 tempLand1이 접근해서 제어하겠다.  
            //생성

            if(Game_Manager.GameMode == 1)
            {
                //메뉴에서 선택된 모드에 따른 지형제작
                LandMake_rand();
            }
            if(Game_Manager.GameMode == 0)
            {
                LandMake_org();

            }
        }
    }
    void LandMake_rand()
    {
        int rand_num = Random.Range(1, Lands.Length); // 0~6 정수 (n-1) 0~5 실수
        //Random.Range(0.0f ,6.0f) // 0 ~ 6 실수
        tempLand2 = Instantiate(Lands[rand_num], new Vector3(30, -2, 0), Quaternion.identity);
    }

    void LandMake_org()
    {
        //if (land_count == StageArray.Length) land_count = 0;
        land_count = land_count % StageArray.Length; // % 6 0,1,2,3,5 

        //StageArray
        tempLand2 = Instantiate(Lands[StageArray[land_count]], new Vector3(30, -2, 0), Quaternion.identity);
        land_count++;
    }

}
