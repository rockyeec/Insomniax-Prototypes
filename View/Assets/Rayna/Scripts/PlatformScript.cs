using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public Transform player;
    public Transform movingPlatform;
    public Transform position1;
    public Transform position2;
    public Transform position3;
    public Vector3 newPosition;
    public string currentState;
    public float smooth;
    public float time;
    bool isMoving;

    PlayerInput speed;

    // Use this for initialization
    void Start()
    {
        Change();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isMoving = false;
        if (isMoving)
        {
            player.position = movingPlatform.position;
        }
        //else if (player.GetComponent<PlayerInput.MoveSpeed>() > 0)
        //{
        //    isMoving = true;
        //}
        
        movingPlatform.position = Vector3.Lerp(movingPlatform.position, newPosition, smooth * Time.deltaTime);
    }

    void Change()
    {
        if (currentState == "Moving To Position 1")
        {
            currentState = "Moving To Position 2";
            newPosition = position2.position;
        }
        else if (currentState == "")
        {
            currentState = "Moving To Position 2";
            newPosition = position2.position;
        }
        Invoke("ChangeTarget", time);
    }
}
