using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesController : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] GameObject glasses = null;
    [SerializeField] Vector3 handPositionOffset = Vector3.zero;
    [SerializeField] Vector3 handEulerOffset = Vector3.zero;

    Transform head;
    Transform hand;

    private void Start()
    {
        head = animator.GetBoneTransform(HumanBodyBones.Head);
        hand = animator.GetBoneTransform(HumanBodyBones.RightHand);
    }

    public void OnOffAnimFinish()
    {
        glasses.SetActive(false);
    }
    public void OnOnAnimFinish()
    {

    }

    public void OnPutOn()
    {
        glasses.transform.SetParent(head);
        glasses.transform.localPosition = Vector3.zero;
        glasses.transform.localRotation = Quaternion.identity;
    }
    public void OnTakeOff()
    {
        glasses.transform.SetParent(hand);
        glasses.transform.localPosition = handPositionOffset;
        glasses.transform.localRotation = Quaternion.Euler(handEulerOffset);
    }

    public void FromPocketOn()
    {
        glasses.SetActive(true);
        glasses.transform.SetParent(hand);
        glasses.transform.localPosition = handPositionOffset;
        glasses.transform.localRotation = Quaternion.Euler(handEulerOffset);
    }
    public void FromPocketOff()
    {
        glasses.transform.SetParent(head);
        glasses.transform.localPosition = Vector3.zero;
        glasses.transform.localRotation = Quaternion.identity;
    }
}
