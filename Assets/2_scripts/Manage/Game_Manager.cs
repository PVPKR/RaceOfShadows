using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ���� ����
using UnityEngine.UI; // UI ��ü ����

public enum GameState {Ready, Go, Play, Over}

public class Game_Manager : MonoBehaviour
{
    public static GameState g_state;
    //�������� �� �� �ִ� ���� - ������ �ʱ�ȭ�� �ڵ����� �Ǿ� �ִ� 
    //�������� ����� �� ������� ���� - �������� ���� : static
    public static PlayerState pstate = PlayerState.init;
    public static int GameMode = 1; //�����÷��� ���ý� 0;

    //������ ȭ�鿡 ǥ���ϰڴ�. - UI
    public Text CoinNum;
    public Text Meter;

    //Ready, Go�� ���̴� ���ڿ�
    public Text txt_Ready;
    public Text txt_Go;
    public Text txt_Over;
    public Image Img_Over;

    public Canvas OverCanvas;
    public Text Over_Coin;
    public Text Over_Meter;
    float overtime;

    public int Coin_Num = 0; // ���� ���� 
    public float Run_Meter = 0; // �̵��Ÿ�



    // Start is called before the first frame update
    void Start()
    {
        g_state = GameState.Ready;
        CoinNum.text = "" + Coin_Num;
        Meter.text = "" + Run_Meter;
        Invoke("ChangeState", 1f);
        OverCanvas.gameObject.SetActive(false);
    }

    void ChangeState()
    {
        if(g_state == GameState.Ready)
        {
            g_state = GameState.Go;
            Invoke("ChangeState", 0.3f);
        }
        else if (g_state == GameState.Go)
        {
            g_state = GameState.Play;
            //Invoke("ChangeState", 0.1f);
        }

    }

    public void GetMoney() //���� �����Ǵ� �Լ��� �ִµ�..
    {
        Coin_Num++;
    }

    public void CalcDistance() // �Ÿ��� �� ����ϴ� �Լ�
    {
        if(g_state == GameState.Play)
        {
            Run_Meter += 3 * Time.deltaTime;
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        ShowUI();
        CalcDistance();
        //print(Run_Meter);

        if(g_state == GameState.Over)
        {
            OnOverLogic();
        }
    }

    void OnOverLogic()
    {
        overtime += Time.deltaTime;
        if(overtime >=3f)
        {
            overtime = 0;
            Go_OverScene();
        }
        Over_Coin.text = "" + Coin_Num;
        Over_Meter.text = "" + Run_Meter;
    }

    void ShowUI()
    { // ���� ���� ���� ���͸� UI ��ü�� �����Ѵ�
        switch (g_state)
        {
            case GameState.Ready:
                    txt_Ready.gameObject.SetActive(true);
                    txt_Go.gameObject.SetActive(false);
                break;
            case GameState.Go:
                    txt_Ready.gameObject.SetActive(false);
                    txt_Go.gameObject.SetActive(true);
                break;
            case GameState.Play:
                txt_Ready.gameObject.SetActive(false);
                txt_Go.gameObject.SetActive(false);
                break;
            case GameState.Over:

                OverCanvas.gameObject.SetActive(true);
                txt_Over.gameObject.SetActive(true);
                Img_Over.gameObject.SetActive(true);
                break;

        }
        CoinNum.text = "" + Coin_Num;
        //Meter.text = "" + Run_Meter;
        Meter.text = string.Format("{0:N2}", Run_Meter);
    }

    public void Go_OverScene()
    {
        SceneManager.LoadScene("4_Over_Scene");
    }

    //��ư �̺�Ʈ���� ����� �Լ�
    public void Go_Pause()
    {
        Time.timeScale = 0f;

    }

    public void Go_Resume()
    {
        Time.timeScale = 1f;
    }

}
