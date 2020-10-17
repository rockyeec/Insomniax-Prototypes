using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput = null;
    [SerializeField] Animator animator = null;
    [SerializeField] GameObject glasses = null;
    [SerializeField] Vector3 handPositionOffset = Vector3.zero;
    [SerializeField] Vector3 handEulerOffset = Vector3.zero;

    Transform head;
    Transform hand;

    string isAnimPlayingString = "isGlassesTransition";
    string putOnString = "Put On Glasses";
    string putOffString = "Put Off Glasses";
    bool isOn = false;
    bool IsTransitioning { get { return animator.GetBool(isAnimPlayingString); } }

    private void Start()
    {
        head = animator.GetBoneTransform(HumanBodyBones.Head);
        hand = animator.GetBoneTransform(HumanBodyBones.RightHand);

        playerInput.OnGlassesButtonPress += PlayerInput_OnGlassesButtonPress;
    }
    private void OnDestroy()
    {
        playerInput.OnGlassesButtonPress -= PlayerInput_OnGlassesButtonPress;
    }

    private void PlayerInput_OnGlassesButtonPress()
    {
        if (IsTransitioning)
            return;

        isOn = !isOn;
        animator.CrossFade(isOn ? putOnString : putOffString, 0.15f);
    }

    public void OnOffAnimFinish()
    {
        glasses.SetActive(false);

        GameScript.TakeOffGlasses();
    }
    public void OnOnAnimFinish()
    {
        GameScript.PutOnGlasses();
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
