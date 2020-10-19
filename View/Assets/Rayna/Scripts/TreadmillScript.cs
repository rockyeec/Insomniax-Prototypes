using UnityEngine;

public class TreadmillScript : MonoBehaviour
{
    [SerializeField] BoxCollider box = null;
    [SerializeField] Transform art = null;

    public BoxCollider Box { get { return box; } }
    public Transform Art { get { return art; } }

    bool isCanMove = false;

    private void Start()
    {
        GameScript.OnGlassesOn += GameScript_OnGlassesOn;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;
    }

    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        isCanMove = false;
    }

    private void GameScript_OnGlassesOn()
    {
        isCanMove = true;
    }

    private void FixedUpdate()
    {
        if (!isCanMove)
            return;

        transform.Translate(Vector3.back * TreadmillRecycler.Speed); 
        if (transform.localPosition.z < TreadmillRecycler.KillSelfZ)
        {
            PlayerInput pI = GetComponentInChildren<PlayerInput>();
            if (pI != null)
            {
                pI.transform.SetParent(null);
            }
            transform.localPosition = TreadmillRecycler.SpawnPoint;
        }
    }
}
