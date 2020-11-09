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

    readonly string isAnimPlayingString = "isGlassesTransition";
    readonly string putOnString = "Put On Glasses";
    readonly string putOffString = "Put Off Glasses";
    bool isOn = false;
    bool IsTransitioning { get { return animator.GetBool(isAnimPlayingString); } }

    private void Start()
    {
        head = animator.GetBoneTransform(HumanBodyBones.Head);
        hand = animator.GetBoneTransform(HumanBodyBones.RightHand);

        playerInput.OnGlassesButtonPress += PlayerInput_OnGlassesButtonPress;

        LoadState();
    }
    private void OnDestroy()
    {
        playerInput.OnGlassesButtonPress -= PlayerInput_OnGlassesButtonPress;
    }

    public void LoadState()
    {
        if (SaveSystem.GetBool("is glasses"))
        {
            glasses.SetActive(true);
            OnPutOn();
            OnOnAnimFinish();
            isOn = true;
        }
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
        //SceneTransitionFader.FadeOutHalf();

        SaveSystem.SetBool("is glasses", false);
    }
    public void OnOnAnimFinish()
    {
        GameScript.PutOnGlasses();
        //SceneTransitionFader.FadeInHalf();

        SaveSystem.SetBool("is glasses", true);
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
