using UnityEngine;

public class HouseScript : MonoBehaviour
{
    [SerializeField] Transform leftHouse = null;
    [SerializeField] Transform rightHouse = null;

    Vector3 a;
    Vector3 b;

    float elapsed = 0.0f;
    float duration = 1.337f;

    private void Start()
    {
        Instantiate(HouseFlyManager.HousePrefabs.GetRandom(), leftHouse).transform.localPosition = Vector3.zero;
        Instantiate(HouseFlyManager.HousePrefabs.GetRandom(), rightHouse).transform.localPosition = Vector3.zero;

        a = Vector3.one;
        b = (Vector3.one * 1.55f).With(x: 1.0f);
        duration = Random.Range(1.6f, 3.3f);

        if (Random.Range(0, 2) == 1)
        {
            SwapAB();
        }
    }

    private void FixedUpdate()
    {
        if (elapsed < duration)
        {
            elapsed += Time.fixedDeltaTime;
            float t = CurveManager.Curve.Evaluate(elapsed / duration);
            leftHouse.localScale = Vector3.LerpUnclamped(a, b, t);
            rightHouse.localScale = Vector3.LerpUnclamped(b, a, t);
        }
        else
        {
            elapsed -= duration;
            SwapAB();
        }
    }

    private void SwapAB()
    {
        Vector3 swap = a;
        a = b;
        b = swap;
    }
}
