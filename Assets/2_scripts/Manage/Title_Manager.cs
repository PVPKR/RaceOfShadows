using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Manager : MonoBehaviour
{
    //=>�ð��� ������ �ڵ����� ���� �ѱ��
    //�ð��� �޴� ����
    //�ð��� �޴� ����
    //���ϴ� �ð�
    //���� �ѱ��

    float elapsedTime;

    //��ü ���� �ʱ�ȭ, ����
    private void Start()
    {
        
    }

    // Update is called once per frame
    //���� ����
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
//    //����ϱ� ���ؼ��� �ش� �ڷ��� Ÿ�� ������ �ʿ� => ���� �� �ʱ�ȭ�� �ʿ�
//    public int ab;
//    public void show() { }


//    //����(static) Ÿ��
//    //��ü�� ���� �ʿ�x ����, �ʱ�ȭ
//    public static int cd = 5; // �ش� ��ü�� ��������� ����.. ������ ������ ���� ����
//    public static void show2(); // �ش� ��ü�� ��������� ����. �Լ��� ������ ������ ����
//}

// ���� ��ȯ�� �ؾ� �Ѵ�.
// ���� �����ϴ� �ڷ���(Ŭ���� �ʿ�) -> ���ǵǾ� �ִ� ����. // �۾�����(���ο� ���ǵ� ��ü)
// ���� ����
// Start is called before the first frame update