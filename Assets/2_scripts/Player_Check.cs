using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Check : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="DeathZone")
        {
            print("ав╬Ы╢ы");
            Game_Manager.g_state = GameState.Over;
        }
    }
}
