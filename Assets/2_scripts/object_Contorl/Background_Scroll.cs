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
        float movrate = mov_speed * Time.deltaTime; // �ʴ� �̵��ӵ�
        //����*��
        //�̵� ������ -> ����
        this.transform.Translate(Vector3.left * movrate);
    }
    void Re_Use_Map()
    {
        //�� ������ ��ġ -30 ���Ϸ� ��������.
        //��ġ�� �� ������ �ʱ� ��ġ�� �ִ´�. (30)
        if (scroll_gap + this.transform.position.x <= 0f) // 0 30
        {
            //���� �� ��ġ�� ����
            this.transform.position += new Vector3(scroll_gap*2, 0, 0);
        }
    }
}
