using UnityEngine;

public class SpinningSegment : MonoBehaviour
{
    [SerializeField] Transform room = null;
    [SerializeField] Transform platform = null;

    [SerializeField] private float spinSpeed = 6.9f;
    [SerializeField] private Vector3 spinAxis = Vector3.forward;

    public Transform Room { get { return room; } }

    private void Start()
    {
        SpinningSegmentArranger.OnStartSpinning += SpinningSegment_OnStartSpinning;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;
        enabled = false;
    }
    private void OnDestroy()
    {
        SpinningSegmentArranger.OnStartSpinning -= SpinningSegment_OnStartSpinning;
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void SpinningSegment_OnStartSpinning()
    {
        enabled = true;
    }
    private void GameScript_OnGlassesOff()
    {
        enabled = false;
    }

    private void FixedUpdate()
    {
        transform.Rotate(spinAxis, Time.fixedDeltaTime * spinSpeed);
        platform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }


}
