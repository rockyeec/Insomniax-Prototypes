using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour
{
    public GameObject cube;

    public bool isFake = false;
    public bool isAbleToFall = false;

    [SerializeField] private Material[] materialList = null;

    MeshRenderer meshRenderer = null;
    Rigidbody rb = null;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "GroundCheck" && isAbleToFall)
        {
            if (rb == null)
            {
                rb = cube.GetComponent<Rigidbody>();
            }
            rb.isKinematic = false;
            rb.AddForce(0, -25, 0, ForceMode.Impulse);
        }
    }

    public void FakeTile(bool isFake)
    {
        if (meshRenderer == null)
        {
            meshRenderer = cube.GetComponent<MeshRenderer>();
        }

        //this.isFake = isFake;
        isAbleToFall = isFake;

        meshRenderer.material = isFake ? materialList[0] : materialList[1];
    }
}
