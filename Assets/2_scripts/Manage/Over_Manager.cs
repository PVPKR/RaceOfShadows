using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Over_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Go_MenuScene()
    {
        SceneManager.LoadScene("2_Menu_Scene");
    }

    public void Go_GameScene()
    {
        SceneManager.LoadScene("3_Game_Scene");
    }
}
