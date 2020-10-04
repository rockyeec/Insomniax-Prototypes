using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    private static TimeHandler instance;
    private float sinTime = 0.0f;
    private float cosTime = 0.0f;
    public static float SinTime { get { return instance.sinTime; } }
    public static float CosTime { get { return instance.cosTime; } }

    public static void Enable()
    {
        instance.enabled = true;
    }

    private void Awake()
    {
        instance = this;
        enabled = false;
    }

    private void Update()
    {
        float time = Time.time * 13.37f;
        sinTime = Mathf.Sin(time);
        cosTime = Mathf.Cos(time);
    }
}
