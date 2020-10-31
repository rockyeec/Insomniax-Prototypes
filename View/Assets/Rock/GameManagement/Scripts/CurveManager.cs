using UnityEngine;

public class CurveManager : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve = null;
    [SerializeField] private AnimationCurve fadeCurve = null;
    [SerializeField] private float animationDuration = 0.69f;
    [SerializeField] private float cameraAnimationDuration = 2.0f;

    private static CurveManager instance = null;

    private void Awake()
    {
        instance = this;

        GameScript.OnGlassesOn += StartLerp;
        GameScript.OnGlassesOff += StartLerp;
        SpinningSegmentArranger.OnStartSpinning += StartLerp;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= StartLerp;
        GameScript.OnGlassesOff -= StartLerp;
        SpinningSegmentArranger.OnStartSpinning -= StartLerp;
    }


    public static AnimationCurve Curve { get { return instance.curve; } }
    public static AnimationCurve FadeCurve { get { return instance.fadeCurve; } }
    public static float AnimationDuration { get { return instance.animationDuration; } }
    public static float CameraAnimationDuration { get { return instance.cameraAnimationDuration; } }

    public static float GlassesAnimT { get { return instance.glassesT; } }

    private float glassesT = 0.0f;
    private float elapsed = float.MaxValue;
    private void Start()
    {
        enabled = false;
    }
    private void StartLerp()
    {
        enabled = true;
        elapsed = 0.0f;
        glassesT = 0.0f;
    }
    private void EndLerp()
    {
        enabled = false;
        glassesT = 1.0f;
    }
    private void Update()
    {
        if (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            glassesT = curve.Evaluate(elapsed / animationDuration);
        }
        else
        {
            EndLerp();
        }
    }
}
