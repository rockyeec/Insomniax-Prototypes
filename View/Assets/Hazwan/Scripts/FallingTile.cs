using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour
{
    public GameObject cube;

    public bool isFake = false;

    [SerializeField] private Material[] materialList = null;

    MeshRenderer ren = null;
    Rigidbody rb = null;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "GroundCheck" && isFake)
        {
            if (rb == null)
            {
                rb = cube.GetComponent<Rigidbody>();
            }
            rb.isKinematic = false;
            rb.AddForce(0, -25, 0, ForceMode.Impulse);
        }
    }

    public void Become(bool isFake)
    {
        if (ren == null)
        {
            ren = cube.GetComponent<MeshRenderer>();
        }

        this.isFake = isFake;

        ren.material = isFake ? materialList[0] : materialList[1];
    }
}
