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
    public Transform target2;

    [SerializeField] Transform cam = null;

    public bool changeCameraView = false;
    public bool changeCameraView2 = false;

    public float speed = 2f;

    bool isPlayable = true;
    public bool isCorrect = false;

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
                cam.position = Vector3.Lerp(cam.position, target.position, speed * Time.deltaTime);
                cam.rotation = Quaternion.Slerp(cam.rotation, target.rotation, speed * Time.deltaTime);
            }
            if (isPlayable)
            {
                StartCoroutine(SwitchCameraView());
                isPlayable = false;
            }
        }
        else if (changeCameraView2)
        {
            if (target2 != null)
            {
                cam.position = Vector3.Lerp(cam.position, target2.position, speed * Time.deltaTime);
                cam.rotation = Quaternion.Slerp(cam.rotation, target2.rotation, speed * Time.deltaTime);
            }
        }
        else
        {
            cam.localPosition = Vector3.Lerp(cam.localPosition, Vector3.zero, speed * Time.deltaTime);
            cam.localRotation = Quaternion.Slerp(cam.localRotation, Quaternion.identity, speed * Time.deltaTime);
            isPlayable = true;
        }
    }

    IEnumerator SwitchCameraView()
    {
        yield return new WaitForSeconds(3f);

        if (isCorrect)
        {
            changeCameraView = false;
            changeCameraView2 = true;
            yield return new WaitForSeconds(3f);
            TriggerDialoguePuzzleLevel3.instance.RemoveFog();
            yield return new WaitForSeconds(3f);
            changeCameraView2 = false;
            PlayerInput.IsEnableCamera = true;
            isCorrect = false;
        }
        else
        {
            changeCameraView2 = false;
            changeCameraView = false;
            PlayerInput.IsEnableCamera = true;
            yield return new WaitForSeconds(2f);
            //LevelManager.ResetLevel();
        }
    }
}
