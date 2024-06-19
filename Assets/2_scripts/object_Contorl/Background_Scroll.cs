using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Scroll : MonoBehaviour
{
    public float mov_speed;
    public float scroll_gap;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Scroll_Mov();
        Re_Use_Map();
    }

    void Scroll_Mov()
    {
        float movrate = mov_speed * Time.deltaTime; // 초당 이동속도
        //방향*힘
        //이동 오른쪽 -> 왼쪽
        this.transform.Translate(Vector3.left * movrate);
    }
    void Re_Use_Map()
    {
        //내 지형의 위치 -30 이하로 내려가면.
        //위치를 전 지형의 초기 위치에 넣는다. (30)
        if (scroll_gap + this.transform.position.x <= 0f) // 0 30
        {
            //현재 내 위치를 조정
            this.transform.position += new Vector3(scroll_gap*2, 0, 0);
        }
    }
}
