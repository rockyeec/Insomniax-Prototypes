using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3rdPerson : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Transform vibrator = null;
    [SerializeField] private Transform rail = null;
    [SerializeField] private Transform pivot = null;

    private float pitch = 0.0f;
    private float yaw = 0.0f;
    private float pitchVel = 0.0f;
    private float yawVel = 0.0f;

    private void Awake()
    {
        cam = Camera.main;
        cam.transform.SetParent(vibrator);
        cam.transform.localPosition = Vector3.zero;
        cam.transform.localRotation = Quaternion.identity;
    }

    public Quaternion GetRotation()
    {
        return cam.transform.rotation;
    }


    public void LookAround(float pitchInput, float yawInput, Vector3 target)
    {
        FollowTarget(target);
        LookAround(pitchInput, yawInput);
    }

    private void FollowTarget(Vector3 target)
    {
        transform.position = target;
    }

    private void LookAround(float pitchInput, float yawInput)
    {
        yaw += yawInput;
        pitch -= pitchInput;
        pitch = Mathf.Clamp(pitch, -69.0f, 69.0f);

        pivot.localEulerAngles = new Vector3(pitch, 0.0f);
        transform.localEulerAngles = new Vector3(0.0f, yaw);
    }
}
