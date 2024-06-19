using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Land_Manager : MonoBehaviour
{
    // �ڷ�ƾ ��ü�� ����� �ڷ�ƾ ��ü�� ���� Ȱ��ȭ �Ѵ�

    //public GameObject Land1; // �������� ����
    //public GameObject Land2;
    //������ ���� ������ ���� �迭 ����
    public GameObject[] Lands;  // = new GameObject[6];

    // ������ ������ ������ �޴� ����
    public GameObject tempLand1;
    public GameObject tempLand2;


    public float scroll_gap; // ������ ũ��
    public float map_count; // ���� ����
    //����ڰ� ���� ���� ����
    int[] StageArray = new int[] { 0, 0, 0, 0, 3, 1, 5, 2, 3, 4, 2, 5, 3, 3, 4, 2, 2, 3, 1 }; // ������ ������ ���ϴ� �迭
    int land_count = 0; // ���� ��ȣ
    void Start()
    {
        

        //for(int i = 0; i<StageArray.Length; i++)
        //{
        //    print("��" + StageArray[i]);
        //}

        tempLand1 = Instantiate(Lands[0]);
        tempLand1.transform.position = new Vector3(0, -2, 0);
        tempLand1.transform.rotation = Quaternion.identity;

        tempLand2 = Instantiate(Lands[0], new Vector3(30, -2, 0), Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
        print("��� :" + Game_Manager.GameMode);
        Re_Use_Map(); // ������ ��ġ üũ�ϴ� �Լ�
    }

    void Re_Use_Map()
    {
        //�� ������ ��ġ -30 ���Ϸ� ��������.
        //��ġ�� �� ������ �ʱ� ��ġ�� �ִ´�. (30)
        if (scroll_gap + tempLand1.transform.position.x <= 0f) // 0 30
        {
            Destroy(tempLand1); // ȭ���� ��� ���� ����
            //
            tempLand1 = tempLand2; // tempLand2�� �����ϴ� ������ tempLand1�� �����ؼ� �����ϰڴ�.  
            //����

            if(Game_Manager.GameMode == 1)
            {
                //�޴����� ���õ� ��忡 ���� ��������
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
        int rand_num = Random.Range(1, Lands.Length); // 0~6 ���� (n-1) 0~5 �Ǽ�
        //Random.Range(0.0f ,6.0f) // 0 ~ 6 �Ǽ�
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
