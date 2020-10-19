using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTileGame : MonoBehaviour
{
    #region Singleton

    private static CameraFollowTileGame _instance;

    public static CameraFollowTileGame Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CameraFollowTileGame>();
            }
            return _instance;
        }
    }
    #endregion

    public Transform target;

    public bool changeCameraView = false;

    public float speed = 2f;

    bool isPlayable = true;

    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        CameraPos();
    }

    public void CameraPos()
    {
        if (changeCameraView)
        {
            if (target != null)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, speed * Time.deltaTime);
            }
            if(isPlayable)
            {
                StartCoroutine(SwitchCameraView());
                isPlayable = false;
            }
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, speed * Time.deltaTime);
            isPlayable = true;
        }
    }

    IEnumerator SwitchCameraView()
    {
        yield return new WaitForSeconds(3f);
        changeCameraView = false;
        PlayerInput.IsEnableCamera = true;
    }
}
