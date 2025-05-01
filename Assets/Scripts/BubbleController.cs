using System.Collections;
using System.Collections.Generic;
using Ommy.Audio;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public delegate void Behaviour();
    public Behaviour currentBehaviour;
    public float changeSize;
    public float minBubbleSize;
    Vector3 playerPos;
    public float bubbleOffsetY ,bubbleOffsetZ;
    public float farwardSpeed;
    public float maxSize;
    private void Start()
    {
        currentBehaviour = FollowPlayer;
        currentBehaviour += IncreaseBubbleSize;
    }
    void Update()
    {
        currentBehaviour?.Invoke();
    }
    public void IncreaseBubbleSize()
    {
        if (Input.GetMouseButton(0) && transform.localScale.x < maxSize)
        {
            transform.localScale += Vector3.one * changeSize * Time.deltaTime;
        }
    }
    public void FollowPlayer()
    {
        playerPos = PlayerMovement.instance.transform.position;
        transform.position = new Vector3(playerPos.x, (playerPos.y + transform.localScale.y / 2) + bubbleOffsetY, (playerPos.z + transform.localScale.z / 2) + bubbleOffsetZ);
    }

    public void SizeDecrement(float decrement)
    {
        if (UIManager.instance.gameState == GameState.GamePlay)
        {
            if (transform.localScale.z > minBubbleSize)
            {
                transform.localScale -= new Vector3(decrement, decrement, decrement);
            }
            else
            {
                GameManager.instance.LevelFail();
            }
        }
    }

    public void MoveFarward()
    {
        transform.position += new Vector3(0, 0, farwardSpeed * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    //public int myInt(){return ClampX;}
}

