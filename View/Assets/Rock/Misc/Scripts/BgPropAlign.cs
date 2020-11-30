using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgPropAlign : MonoBehaviour
{
    [SerializeField] Transform startPoint = null;
    [SerializeField] BgPropLeftRight wanderer = null;
    [SerializeField] Transform[] platforms = null;

    static BgPropAlign instance;
    private void Awake()
    {
        instance = this;

        for (int i = 0; i < platforms.Length; i++)
        {
            a.Add(Vector3.zero);
            b.Add(Vector3.zero);
            aRot.Add(Quaternion.identity);
        }

        enabled = false;
    }

    public static void Trigger()
    {
        instance.wanderer.enabled = false;
        instance.StartLerp();
    }

    List<Quaternion> aRot = new List<Quaternion>();
    List<Vector3> a = new List<Vector3>();
    List<Vector3> b = new List<Vector3>();
    float elapsed = float.MaxValue;
    readonly float duration = 3.2f;


    void StartLerp()
    {
        enabled = true;
        elapsed = 0.0f;

        for (int i = 0; i < platforms.Length; i++)
        {
            a[i] = platforms[i].position;
            b[i] = startPoint.position + Vector3.forward * i * platforms[i].localScale.z;
            aRot[i] = platforms[i].rotation;
        }
    }
    void EndLerp()
    {
        enabled = false;
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].position = b[i];
            platforms[i].rotation = Quaternion.identity;
        }
    }

    private void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = CurveManager.Curve.Evaluate(elapsed / duration);
            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].position = Vector3.LerpUnclamped(a[i], b[i], t);
                platforms[i].rotation = Quaternion.SlerpUnclamped(aRot[i], Quaternion.identity, t);
            }
        }
        else
        {
            EndLerp();
        }
    }
}
