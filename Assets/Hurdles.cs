using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            UIManager.instance.gameState = GameState.LevelFail;
            print("here is level fail");
            GameManager.instance.LevelFail();

            PlayerMovement.instance.IsWalking = false;
            PlayerMovement.instance.PlayerAnim.SetBool("isRunning", false);
        }
       
    }
}
