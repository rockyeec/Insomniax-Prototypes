﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3rdPerson : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Transform vibrator = null;
    [SerializeField] private Transform rail = null;
    [SerializeField] private Transform railRef = null;
    [SerializeField] private Transform pivot = null;

    private float pitch = 0.0f;
    private float yaw = 0.0f;

    private Vector3[] clipPoints;
    private readonly float railDefaultDistance = 2.69f;

    private void Awake()
    {
        cam = Camera.main;
        cam.transform.SetParent(vibrator);
        cam.transform.localPosition = Vector3.zero;
        cam.transform.localRotation = Quaternion.identity;
    }

    private void Start()
    {
        InitializeClipPoints();

        rail.localPosition = railRef.localPosition = Vector3.back * railDefaultDistance;
    }

    private void InitializeClipPoints()
    {
        float z = cam.nearClipPlane;
        float y = z * Mathf.Tan(cam.fieldOfView / 2 * Mathf.Deg2Rad);
        float x = y * cam.aspect;

        clipPoints = new Vector3[5]
            {
                // center
                new Vector3(0.0f, 0.0f, z),
                // top left
                new Vector3(-x, y, z),
                // top right
                new Vector3(x, y, z),
                // bottom left
                new Vector3(-x, -y, z),
                // bottom right
                new Vector3(x, -y, z),
            };
    }

    public Quaternion GetRotation()
    {
        return cam.transform.rotation;
    }

    // Camera Update function
    public void LookAround(float pitchInput, float yawInput, in Vector3 target, float delta)
    {
        FollowTarget(in target);
        LookAround(pitchInput, yawInput);
        ReactToWall(delta);
    }

    private void FollowTarget(in Vector3 target)
    {
        transform.position = target;
    }

    private void LookAround(float pitchInput, float yawInput)
    {
        yaw += yawInput;
        pitch -= pitchInput;
        pitch = Mathf.Clamp(pitch, -42.0f, 42.0f);

        pivot.localEulerAngles = new Vector3(pitch, 0.0f);
        transform.localEulerAngles = new Vector3(0.0f, yaw);
    }

    private void ReactToWall(float delta)
    {
        float distance = railDefaultDistance;
        foreach (var item in clipPoints)
        {
            Vector3 point = railRef.rotation * item + railRef.position;

            //Debug.DrawLine(pivot.transform.position, point);
            
            if (Physics.Linecast(pivot.transform.position, point, out RaycastHit hit, 1 << 0))
            {
                if (hit.distance < distance)
                {
                    distance = hit.distance * 0.69f;
                }
            }
        }

        rail.localPosition = Vector3.Lerp(rail.localPosition, Vector3.back * distance, delta * 13.37f);
    }
}
