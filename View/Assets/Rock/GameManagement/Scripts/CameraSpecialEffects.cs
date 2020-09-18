using System.Collections;
using UnityEngine;

public class CameraSpecialEffects : MonoBehaviour
{
    Camera cam;
    readonly private float duration = 0.69f;

    readonly private float defaultCameraFov = 60.0f;
    readonly private float zoomedOutFov = 133.7f;

    private Quaternion downRot;
    private Quaternion forwardRot;

    readonly private Vector3 upPos = new Vector3(0.0f, 6.9f);
    readonly private Vector3 normPos = new Vector3(0.0f, 0.0f);


    private void Awake()
    {
        cam = Camera.main;

        downRot = Quaternion.LookRotation(Vector3.down);
        forwardRot = Quaternion.LookRotation(Vector3.forward);
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
        float startFov = cam.fieldOfView;
        Quaternion startRot = cam.transform.rotation;
        Vector3 startPos = cam.transform.localPosition;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            float t = elapsed / duration;

            cam.fieldOfView = Mathf.Lerp(startFov, zoomedOutFov, t);

            cam.transform.rotation = Quaternion.Slerp(startRot, downRot, t);

            cam.transform.localPosition = Vector3.Lerp(startPos, upPos, t);

            yield return null;
        }
        cam.fieldOfView = zoomedOutFov;
        cam.transform.rotation = downRot;
        cam.transform.localPosition = upPos;
    }
    private IEnumerator CameraGoDownNZoomIn()
    {
        float elapsed = 0.0f;
        float startFov = cam.fieldOfView;
        Quaternion startRot = cam.transform.localRotation;
        Vector3 startPos = cam.transform.localPosition;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            float t = elapsed / duration;

            cam.fieldOfView = Mathf.Lerp(startFov, defaultCameraFov, t);

            cam.transform.localRotation = Quaternion.Slerp(startRot, forwardRot, t);

            cam.transform.localPosition = Vector3.Lerp(startPos, normPos, t);

            yield return null;
        }
        cam.fieldOfView = defaultCameraFov;
        cam.transform.localRotation = forwardRot;
        cam.transform.localPosition = normPos;
    }
}
