using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���¸� �����ϰ� 
public enum PlayerState { init, run, jump, djump, slide } // �÷��̾� ���� ����.. �׾��� .fevertime
public class Player_Control : MonoBehaviour
{
    //�ִϸ��̼��� �����ϱ����� ����
    public Animator playerAni;

    //���带 ������ ����
    public AudioSource JumpSound;
    public AudioSource CoinSound;

    // ��Ȳ�� ����.. ���� ���� ���



    //���¸� üũ�� ������ �����
    //PlayerState pstate = PlayerState.init; //�⺻�� �� ���� //���ӸŴ����� �÷��̾ �������� ����ϰ� �ʹ�.

    Rigidbody player_rig;
    int jumpcount = 0; // ������ Ƚ���� ������ ����
    //�浹ü�� ������ �޾Ƽ� -ok
    public BoxCollider SlideCol; //�����̵��
    public CapsuleCollider RunCol; // �Ϲݿ�

    Game_Manager main_logic; // ������ �ִ°�? �ʱ�ȭ�� ������ �ȵ� ������ �׳� ������
    //�ҽ� - ��ü�� ����Ǿ� ����
    //��ü�� ������ ����... 
    //������ ������ �� ������ �� �ִ�.
    GameObject MainLogic; // ��ü�� ������ �� �����ϱ� ���� ����


    // Start is called before the first frame update
    void Start() // ��ü�� ��������ڸ��� 1��
    {
        //�ִϸ��̼� ��Ʈ�� ��ü�� ã�Ƽ� �����Ѵ�.
        playerAni = this.GetComponentInChildren<Animator>();

        //��ü�� ������ ã�´�.
        MainLogic = GameObject.Find("Game_Manager"); // Hirachy���� ��ü�� �̸����� ã�´�.
        //��ü ������ ���� Ŭ���� ������ ã�´�.
        main_logic = MainLogic.GetComponent<Game_Manager>();
        //this.transform.Find(""); // ���õ� ��ü ������ �̸����� ã�´� _�ڽİ�ü �˻�

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
        //print("������� : " + Game_Manager.pstate);
        //�� �����Ӹ��� �������� ��Ȱ��ȭ�Ȱ��� Ȱ��ȭ ��Ų��
        player_rig.WakeUp();
        JumpContorl();
        SlideContorl();
    }

    //Ű�Է��� ���� ���� �����Ǹ�
    //�� ��ü�� ���� (���� �ö󰣴�)
    //�߷��� ������ �ֱ� ������ �߷��� �Ž����� ���� �ο��ؼ� ���� �ø���.
    //�� ��ü(�÷��̾�)�� ������ ��� ������??
    void JumpContorl()
    {
        //Ű �Է¿� ����
        //�����Ӽ��� ���. -ok
        //�߷°� �ݴ��� ���� �ο��Ѵ�
        //���͹��(����+��), //���� ���� ������... Ű����� ���� (���� ũ��� 1��)
        //Ű�Է�, Input Ŭ����
        //Input.getButtonDown("Jump"); // input �Ӽ��� ���ǵ�... Ű����
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


        if (Input.GetKeyDown(KeyCode.LeftControl) == true) //��Ȳ�� ����... Ű�Է�
        {
            if(playerAni.GetBool("Slide") == false)
            {
                print("�����̵崭����");
                playerAni.SetBool("Slide", true);
                SlideCol.enabled = true;
                RunCol.enabled = false;
                //1~2�ʰ� ������ ���󺹱�
                //Invoke("Init_Collider", 2f); //2���Ŀ� �Լ��� ȣ��
                //�ڷ�ƾ...
                StartCoroutine("initCol");
            }
        }
    }

    IEnumerator initCol()
    {
        Game_Manager.pstate = PlayerState.slide; // �Լ� ���� -> �����̵�
        // ���ǿ� ���� ����. ����Ǵ� �κ�
        yield return new WaitForSeconds(1.5f); // ��
        //return ���ǿ� �������� �� ����Ǵ� �κ�

        Init_Collider();
        playerAni.SetBool("Slide", false);
        Game_Manager.pstate = PlayerState.run; // ���󺹱� �� -> �� ����

        playerAni.SetTrigger("Run");

    }

    //�̺�Ʈ : ���α׷� ������ �پ��� ��Ȳ�� �߻��� ��, �ڵ������� ȣ��Ǵ� �Լ�, ��ü...
    //�̺�Ʈ ���� �Լ� ->On�̺�Ʈ��
    //collider : ������ ��ü, trigger : ��ü(���ɰ���)-������ ������, ���濡 ������ �� �ִ�
    //�浹 ; Collsion (��ü), Trigger (��ü)
    // ���� ; Enter(����), Stay(����), Exit(Ż��)->�浹 ���̾��ٰ� Ż��Ǵ� ��

    // c-c - collision // c- t, t-c, t-t:trigger 
    private void OnCollisionEnter(Collision collision)
    {
        //���� collision
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
        //���� other
        if (other.gameObject.tag == "Coins")
        {
            //print("������ �浹�߾�");
            CoinSound.Play();
            Destroy(other.gameObject); //��ü�� ����� �Լ�
            //���� ������Ų�� - Game_Mgr
            main_logic.GetMoney();


        }
        //else
            //print("��ü���� �Ͱ� �浹����" + other.gameObject.name);
    }
}
