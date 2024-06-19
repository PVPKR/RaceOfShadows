using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//상태를 정의하고 
public enum PlayerState { init, run, jump, djump, slide } // 플레이어 상태 추후.. 죽었다 .fevertime
public class Player_Control : MonoBehaviour
{
    //애니메이션을 제어하기위한 변수
    public Animator playerAni;

    //사운드를 제어할 변수
    public AudioSource JumpSound;
    public AudioSource CoinSound;

    // 상황에 따라.. 점프 사운드 재생



    //상태를 체크할 변수를 만든다
    //PlayerState pstate = PlayerState.init; //기본은 런 상태 //게임매니저도 플레이어도 공통으로 사용하고 싶다.

    Rigidbody player_rig;
    int jumpcount = 0; // 점프의 횟수를 가지는 변수
    //충돌체의 정보를 받아서 -ok
    public BoxCollider SlideCol; //슬라이드용
    public CapsuleCollider RunCol; // 일반용

    Game_Manager main_logic; // 정보가 있는가? 초기화나 세팅이 안된 변수는 그냥 껍데기
    //소스 - 객체에 연결되어 있음
    //객체의 정보를 얻어야... 
    //내부의 정보를 얻어서 세팅할 수 있다.
    GameObject MainLogic; // 객체의 정보를 얻어서 세팅하기 위한 변수


    // Start is called before the first frame update
    void Start() // 객체가 만들어지자마자 1번
    {
        //애니메이션 콘트를 객체를 찾아서 연결한다.
        playerAni = this.GetComponentInChildren<Animator>();

        //객체를 정보를 찾는다.
        MainLogic = GameObject.Find("Game_Manager"); // Hirachy에서 객체를 이름으로 찾는다.
        //객체 내에서 내부 클래스 정보를 찾는다.
        main_logic = MainLogic.GetComponent<Game_Manager>();
        //this.transform.Find(""); // 선택된 객체 내에서 이름으로 찾는다 _자식객체 검색

        PlayerState pstate = PlayerState.init;
        player_rig = this.GetComponent<Rigidbody>();
        Init_Collider();

        playerAni.SetTrigger("Run");
    }

    void Init_Collider()
    {
        //this.gameObject.SetActive(); 
        SlideCol.enabled = false;
        RunCol.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        //print("현재상태 : " + Game_Manager.pstate);
        //매 프레임마다 물리력이 비활성화된것을 활성화 시킨다
        player_rig.WakeUp();
        JumpContorl();
        SlideContorl();
    }

    //키입력을 눌린 것이 감지되면
    //내 객체가 점프 (위로 올라간다)
    //중력을 가지고 있기 때문에 중력을 거스르는 힘을 부여해서 위로 올린다.
    //내 객체(플레이어)의 점프를 어떻게 얻을까??
    void JumpContorl()
    {
        //키 입력에 따라
        //물리속성을 얻고. -ok
        //중력과 반대의 힘을 부여한다
        //벡터방식(방향+힘), //자주 쓰는 방향은... 키워드로 구현 (힘의 크기는 1임)
        //키입력, Input 클래스
        //Input.getButtonDown("Jump"); // input 속성에 정의된... 키워드
        //Input.GetKeyDown();

        //if (Input.GetButtonDown("Jump") == true)

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            if (Game_Manager.pstate == PlayerState.run)
            {
                Game_Manager.pstate = PlayerState.jump;
                playerAni.SetBool("Jump", true);
                player_rig.AddForce(Vector3.up * 300f);
                JumpSound.playOnAwake = false;
                JumpSound.Play();
            }
            else if(Game_Manager.pstate == PlayerState.jump)
            {
                Game_Manager.pstate = PlayerState.djump;
                playerAni.SetBool("DJump", true);
                player_rig.AddForce(Vector3.up * 300f);
                JumpSound.Play();
            }



        }


    }

    void SlideContorl()
    {


        if (Input.GetKeyDown(KeyCode.LeftControl) == true) //상황에 따라... 키입력
        {
            if(playerAni.GetBool("Slide") == false)
            {
                print("슬라이드눌럿냐");
                playerAni.SetBool("Slide", true);
                SlideCol.enabled = true;
                RunCol.enabled = false;
                //1~2초가 지나면 원상복귀
                //Invoke("Init_Collider", 2f); //2초후에 함수를 호출
                //코루틴...
                StartCoroutine("initCol");
            }
        }
    }

    IEnumerator initCol()
    {
        Game_Manager.pstate = PlayerState.slide; // 함수 시작 -> 슬라이드
        // 조건에 관계 없이. 수행되는 부분
        yield return new WaitForSeconds(1.5f); // 초
        //return 조건에 만족했을 때 수행되는 부분

        Init_Collider();
        playerAni.SetBool("Slide", false);
        Game_Manager.pstate = PlayerState.run; // 원상복귀 후 -> 런 상태

        playerAni.SetTrigger("Run");

    }

    //이벤트 : 프로그램 내에서 다양한 상황이 발생할 때, 자동적으로 호출되는 함수, 객체...
    //이벤트 동작 함수 ->On이벤트명
    //collider : 딱딱한 강체, trigger : 기체(유령같은)-검출은 되지만, 상대방에 영향을 안 주는
    //충돌 ; Collsion (강체), Trigger (기체)
    // 상태 ; Enter(진입), Stay(교차), Exit(탈출)->충돌 중이었다가 탈출되는 것

    // c-c - collision // c- t, t-c, t-t:trigger 
    private void OnCollisionEnter(Collision collision)
    {
        //상대방 collision
        if (collision.gameObject.tag == "Land")
        {
            if (Game_Manager.pstate == PlayerState.jump || Game_Manager.pstate == PlayerState.djump)
            {
                playerAni.SetBool("Jump", false);
                playerAni.SetBool("DJump", false);
                Game_Manager.pstate = PlayerState.run;
                playerAni.SetTrigger("Run");
            }

            if (Game_Manager.pstate == PlayerState.init)
            {
                Game_Manager.pstate = PlayerState.run;
                playerAni.SetTrigger("Run");
            }

        }



    }

    private void OnTriggerEnter(Collider other)
    {
        //상대방 other
        if (other.gameObject.tag == "Coins")
        {
            //print("동전과 충돌했어");
            CoinSound.Play();
            Destroy(other.gameObject); //객체를 지우는 함수
            //돈을 증가시킨다 - Game_Mgr
            main_logic.GetMoney();


        }
        //else
            //print("기체같은 것과 충돌됐으" + other.gameObject.name);
    }
}
