using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land_Contorl : MonoBehaviour
{
    //�ӵ�
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
        float movrate = mov_speed * Time.deltaTime; // �ʴ� �̵��ӵ�
        //����*��
        //�̵� ������ -> ����
        this.transform.Translate(Vector3.left * movrate);
    }

}
