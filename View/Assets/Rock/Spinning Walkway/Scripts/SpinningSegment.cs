using UnityEngine;

public class SpinningSegment : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 6.9f;

    private void Start()
    {
        GameScript.OnGlassesOn += GameScript_OnGlassesOn;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;
        enabled = false;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOn()
    {
        enabled = true;
    }
    private void GameScript_OnGlassesOff()
    {
        enabled = false;
    }

    private void FixedUpdate()
    {
        transform.Rotate(transform.forward, Time.fixedDeltaTime * spinSpeed);
    }
}
