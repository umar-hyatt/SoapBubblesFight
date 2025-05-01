using System.Collections;
using System.Collections.Generic;
using Ommy.Audio;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public static BubbleSpawner instance;
    public GameObject bubblePrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SpawnBubble()
    {
        if (bubblePrefab == null)
        {
            Debug.LogError("Bubble prefab is not assigned!");
            return;
        }

        var obj = Instantiate(bubblePrefab, Vector3.zero, Quaternion.identity);
        if (PlayerMovement.instance != null)
        {
            if(PlayerMovement.instance.handBubble != null) 
                PlayerMovement.instance.handBubble.currentBehaviour = PlayerMovement.instance.handBubble.MoveFarward;
            
            PlayerMovement.instance.handBubble = obj.GetComponent<BubbleController>();
        }
        else
        {
            Debug.LogError("PlayerMovement instance is null!");
        }
    }

    public void Update()
    {
        if (UIManager.instance.gameState != GameState.GamePlay) return;

        if (Input.GetMouseButton(0))
        {
            // Placeholder for future logic
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (PlayerMovement.instance?.handBubble != null)
            {
                AudioManager.Instance.PlaySFX(SFX.bubbleShoot);
                PlayerMovement.instance.handBubble.currentBehaviour += PlayerMovement.instance.handBubble.MoveFarward;
                PlayerMovement.instance.handBubble.currentBehaviour -= PlayerMovement.instance.handBubble.FollowPlayer;
            }
            else
            {
                Debug.LogWarning("HandBubble is null or PlayerMovement instance is null!");
            }
            SpawnBubble();
        }
    }
}
