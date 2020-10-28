using UnityEngine;

public class PlatformHallway : MonoBehaviour
{
    [SerializeField] BoxCollider box = null;
    [SerializeField] Transform art = null;

    public void Refresh()
    {
        if (transform.localScale == Vector3.one)
            return;

        box.size = transform.localScale;
        art.localScale = transform.localScale;
        transform.localScale = Vector3.one;
    }
}
