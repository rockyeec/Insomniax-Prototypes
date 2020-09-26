using UnityEngine;

public class CurveManager : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve = null;

    private static CurveManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    public static AnimationCurve Curve { get { return instance.curve; } }
}
