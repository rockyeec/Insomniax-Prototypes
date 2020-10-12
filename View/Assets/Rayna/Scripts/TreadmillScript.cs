using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillScript : MonoBehaviour
{
    [SerializeField] BoxCollider box = null;
    [SerializeField] Transform art = null;

    public BoxCollider Box { get { return box; } }
    public Transform Art { get { return art; } }

    //bool isCanMove = false;

    //private void Start()
    //{
    //    GameScript.OnGlassesOn += GameScript_OnGlassesOn;
    //    GameScript.OnGlassesOff += GameScript_OnGlassesOff;
    //}

    //private void OnDestroy()
    //{
    //    GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
    //    GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    //}

    //private void GameScript_OnGlassesOff()
    //{
    //    isCanMove = false;
    //    // do whatever here
    //}

    //private void GameScript_OnGlassesOn()
    //{
    //    // and here so example you want the platform to not move when glasses are off
    //    isCanMove = true;
    //}

    private void FixedUpdate()
    {
        //if (!isCanMove)
        //    return; // lets try it

        transform.Translate(Vector3.back * TreadmillRecycler.Speed); // so that you only need to adjust at one place, which is the recycler
        if (transform.localPosition.z < TreadmillRecycler.KillSelfZ)
        {
            PoppingNames pN = GetComponentInChildren<PoppingNames>();
            if (pN != null)
            {
                pN.transform.parent.gameObject.SetActive(false);
            }


            PlayerInput pI = GetComponentInChildren<PlayerInput>();
            if (pI != null)
            {
                pI.transform.SetParent(null);
            }
            transform.localPosition = TreadmillRecycler.SpawnPoint;
        }
    }
}
