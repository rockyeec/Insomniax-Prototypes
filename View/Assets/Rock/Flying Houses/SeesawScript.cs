using UnityEngine;

public class SeesawScript : MonoBehaviour
{
    [SerializeField] float deltaAngle = 6.9f;
    [SerializeField] float duration = 1.337f;

    Quaternion a, b;
    float elapsed = 0.0f;

    private void Start()
    {
        a = Quaternion.AngleAxis(deltaAngle, transform.right);
        b = Quaternion.AngleAxis(-deltaAngle, transform.right);
    }

    private void FixedUpdate()
    {
        if (elapsed < duration)
        {
            elapsed += Time.fixedDeltaTime;
            MoveTowardsB();
        }
        else
        {
            elapsed -= duration;
            transform.localRotation = b;
            SwapAB();
        }
    }

    void MoveTowardsB()
    {
        float t = CurveManager.Curve.Evaluate(elapsed / duration);
        transform.localRotation = Quaternion.Slerp(a, b, t);
    }
    void SwapAB()
    {
        Quaternion swap = a;
        a = b;
        b = swap;
    }
}
