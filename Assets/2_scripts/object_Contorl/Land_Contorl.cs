using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land_Contorl : MonoBehaviour
{
    //속도
    public float mov_speed=5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Scroll_Mov();
    }

    void Scroll_Mov()
    {
        float movrate = mov_speed * Time.deltaTime; // 초당 이동속도
        //방향*힘
        //이동 오른쪽 -> 왼쪽
        this.transform.Translate(Vector3.left * movrate);
    }

}
