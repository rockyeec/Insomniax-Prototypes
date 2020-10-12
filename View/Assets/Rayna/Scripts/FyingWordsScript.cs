using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FyingWordsScript : MonoBehaviour
{
    public GameObject player;
    public Transform playerPos;
    public float speed = 1.0f;

    bool isTouch = false;

    
    void Start()
    {
        playerPos = player.transform;
    }

    void Update()
    {
        if (player)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
            if (gameObject.transform.position == player.transform.position)
            {
                isTouch = true;
            }
        }

        if(isTouch)
        {
            Destroy(gameObject, 1f);
        }
    }

    

}
