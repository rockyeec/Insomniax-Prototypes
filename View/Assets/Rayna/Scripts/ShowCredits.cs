using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowCredits : MonoBehaviour
{
    public Transform player;
    public float showDist;
    MeshRenderer textMesh;

//----------------------------------------------------------------------------------
    void Start()
    {
        textMesh = gameObject.GetComponent<MeshRenderer>();
    }

    //----------------------------------------------------------------------------------
    void Update()
    {

        if (Vector3.Distance(transform.position, player.position) < showDist)
        {
            textMesh.enabled = true;
            StartCoroutine("WaitForSec");
        }
        else
        { 
            textMesh.enabled = false;
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
    }
    //public GameObject uiNames;
    //void Start()
    //{
    //    uiNames.SetActive(false);
    //}

    //private void OnTriggerEnter(Collider player)
    //{
    //    if (player.gameObject.tag == "Player")
    //    {
    //        uiNames.SetActive(true);
    //        StartCoroutine("WaitForSec");
    //    }
    //}

    //IEnumerator WaitForSec()
    //{
    //    yield return new WaitForSeconds(5);
    //    Destroy(uiNames);
    //}
}
