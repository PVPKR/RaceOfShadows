using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리 참조
using UnityEngine.UI; // UI 객체 참조

public enum GameState {Ready, Go, Play, Over}

public class Game_Manager : MonoBehaviour
{
    public static GameState g_state;
    //공통으로 쓸 수 있는 변수 - 생성과 초기화가 자동으로 되어 있는 
    //컨텐츠가 종료될 때 사라지는 변수 - 전역같은 변수 : static
    public static PlayerState pstate = PlayerState.init;
    public static int GameMode = 1; //게임플레이 선택시 0;

    //정보를 화면에 표시하겠다. - UI
    public Text CoinNum;
    public Text Meter;

    //Ready, Go가 보이는 문자열
    public Text txt_Ready;
    public Text txt_Go;
    public Text txt_Over;
    public Image Img_Over;

    public Canvas OverCanvas;
    public Text Over_Coin;
    public Text Over_Meter;
    float overtime;

    public int Coin_Num = 0; // 동전 갯수 
    public float Run_Meter = 0; // 이동거리



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

    public void GetMoney() //돈이 증가되는 함수가 있는데..
    {
        Coin_Num++;
    }

    public void CalcDistance() // 거리를 얻어서 계산하는 함수
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
    { // 현재 동전 수와 미터를 UI 객체에 세팅한다
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

    //버튼 이벤트에서 사용할 함수
    public void Go_Pause()
    {
        Time.timeScale = 0f;

    }

    public void Go_Resume()
    {
        Time.timeScale = 1f;
    }

}
