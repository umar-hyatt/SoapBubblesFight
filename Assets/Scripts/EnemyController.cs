using System.Collections;
using System.Collections.Generic;
using Ommy.Audio;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public float dicreamentSize;
    public float speed, upwardSpeed = 5f;
    public GameObject bubble;
    public float chaseRange;
    public Rigidbody rb;
    public static Transform target;
    public bool inBubble;
    bool isCollided = false, isRunning = true, isFirstCollision = true;
    private void Start()
    {
        target = PlayerMovement.instance.transform;
    }
    private void Update()
    {
        
        if (UIManager.instance.gameState != GameState.GamePlay)
            return;

        if (Vector3.Distance(PlayerMovement.instance.transform.position, transform.position) < chaseRange)
        {
            if (target != null & isRunning == true)
            {   //Debug.Log ("running");
                GetComponent<Animator>().SetBool("isRunning", true);
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                transform.LookAt(target.position);
                //rb.MovePosition(target.position);
            }
            
            if (transform.position.z < target.position.z - 200)
            {
                Debug.Log(gameObject.name + " Destroyed");
                gameObject.SetActive(false);
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("isRunning", false);
        }


        if (isCollided == true)
        {
            //Debug.Log("collide");
            bubble.SetActive(true);
            rb.useGravity = false;
            transform.position += new Vector3(0, upwardSpeed * Time.deltaTime, 0);
            GetComponent<Animator>().SetBool("isFalling", true);
            GetComponent<Animator>().SetBool("isRunning", true);

            Destroy(gameObject, 3);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(inBubble) return;
        if (other.transform.tag == "Bubble")
        {
            if (isFirstCollision == true)
            {
                isFirstCollision = false;
                //other.GetComponent<BubbleController>().SizeDecrement(dicreamentSize);
                if(other.GetComponent<BubbleController>() != null)
                {
                    other.GetComponent<BubbleController>().SizeDecrement(dicreamentSize);
                }
                isRunning = false;
                isCollided = true;
                inBubble = true;
                AudioManager.Instance.PlaySFX(SFX.bubbleCook);
                // target = null;
                Destroy(other.gameObject);
            }
        }
        else if (other.transform.tag == "Player")
        {
            UIManager.instance.gameState = GameState.LevelFail;
            Debug.Log(name+" p "+other.name);
            //Debug.Break();
            GameManager.instance.LevelFail();
        }
    }
}



