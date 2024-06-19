using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Go_GameOrigin()
    {
        Game_Manager.GameMode = 0;
        SceneManager.LoadScene("3_Game_Scene");
    }

    public void Go_GameRnd()
    {
        Game_Manager.GameMode = 1;
        SceneManager.LoadScene("3_Game_Scene");
    }
}
