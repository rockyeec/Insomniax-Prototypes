using UnityEngine;

public class HouseScript : MonoBehaviour
{
    Vector3 target;
    Vector3 source;

    float elapsed = 0.0f;
    float duration = 1.337f;

    float depth = 0.69f;

    private void Start()
    {
        GameScript.OnGlassesOn += GameScript_OnGlassesOn;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;

        Instantiate(HouseFlyManager.HousePrefabs.GetRandom(), transform).transform.localPosition = Vector3.zero;

        RandomizeTarget();
        source = transform.localScale;

        ResetHouse();
        enabled = false;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        ResetHouse();
        enabled = false;
    }

    private void GameScript_OnGlassesOn()
    {
        enabled = true;
    }

    private void ResetHouse()
    {
        if (Mathf.Abs(source.z) > Mathf.Abs(target.z))
        {
            SwapSourceTarget();
            RandomizeTarget();
        }
        elapsed = 0.0f;

        duration = Random.Range(1.2f, 4.5f);
        depth = Random.Range(0.5f, 1.8f);
    }

    private void RandomizeTarget()
    {
        target = Vector3.one * Random.Range(1.05f, 1.45f);
    }

    private void FixedUpdate()
    {
        if (elapsed < duration)
        {
            elapsed += Time.fixedDeltaTime;
            transform.localScale = Vector3.LerpUnclamped(
                source,
                target,
                CurveManager.Curve.Evaluate(elapsed / duration));
        }
        else
        {
            elapsed -= duration;
            SwapSourceTarget();
        }

        if (transform.localPosition.z < depth)
            transform.localPosition += Vector3.forward * Time.deltaTime * 0.16f;
    }

    private void SwapSourceTarget()
    {
        Vector3 swap = target;
        target = source;
        source = swap;
    }
}
