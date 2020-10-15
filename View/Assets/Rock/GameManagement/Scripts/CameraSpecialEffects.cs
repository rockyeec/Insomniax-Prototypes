using System.Collections;
using UnityEngine;

public class CameraSpecialEffects : MonoBehaviour
{
    private Camera cam = null;

    readonly private float defaultCameraFov = 60.0f;
    readonly private float zoomedOutFov = 133.7f;

    private Quaternion downRot;
    private Quaternion forwardRot;
    private Color downColor;

    readonly private Vector3 upPos = new Vector3(20.9f, 13.37f);
    readonly private Vector3 normPos = new Vector3(0.0f, 0.0f);
    readonly private Color upColor = Color.white;


    private void Awake()
    {
        cam = Camera.main;

        downRot = Quaternion.LookRotation(Vector3.down);
        forwardRot = Quaternion.LookRotation(Vector3.forward);
        downColor = cam.backgroundColor;
    }

    public void ZoomOut()
    {
        StopAllCoroutines();
        StartCoroutine(CameraGoUpNZoomOut());
    }

    public void ZoomIn()
    {
        StopAllCoroutines();
        StartCoroutine(CameraGoDownNZoomIn());
    }

    private IEnumerator CameraGoUpNZoomOut()
    {
        float elapsed = 0.0f;
        float duration = CurveManager.CameraAnimationDuration;
        float startFov = cam.fieldOfView;
        Quaternion startRot = cam.transform.rotation;
        Vector3 startPos = cam.transform.position;
        Vector3 targetPos = cam.transform.position + upPos;
        Color startCol = cam.backgroundColor;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            float t = elapsed / duration;
            t = CurveManager.Curve.Evaluate(t);

            cam.fieldOfView = Mathf.Lerp(startFov, zoomedOutFov, t);
            cam.transform.rotation = Quaternion.Slerp(startRot, downRot, t);
            cam.transform.position = Vector3.Lerp(startPos, targetPos, t);
            cam.backgroundColor = Color.Lerp(startCol, upColor, t);

            yield return null;
        }
        cam.fieldOfView = zoomedOutFov;
        cam.transform.rotation = downRot;
        cam.transform.position = targetPos;
        cam.backgroundColor = upColor;
    }
    private IEnumerator CameraGoDownNZoomIn()
    {
        float elapsed = 0.0f;
        float duration = 1.2f;
        float startFov = cam.fieldOfView;
        Quaternion startRot = cam.transform.localRotation;
        Vector3 startPos = cam.transform.localPosition;
        Color startCol = cam.backgroundColor;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            float t = elapsed / duration;
            t = CurveManager.Curve.Evaluate(t);

            cam.fieldOfView = Mathf.LerpUnclamped(startFov, defaultCameraFov, t);
            cam.transform.localRotation = Quaternion.SlerpUnclamped(startRot, forwardRot, t);
            cam.transform.localPosition = Vector3.LerpUnclamped(startPos, normPos, t);
            cam.backgroundColor = Color.Lerp(startCol, downColor, t);

            yield return null;
        }
        cam.fieldOfView = defaultCameraFov;
        cam.transform.localRotation = forwardRot;
        cam.transform.localPosition = normPos;
        cam.backgroundColor = downColor;
    }
}
