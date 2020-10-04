using UnityEngine;

public class TriggerForPoppingName : MonoBehaviour
{
    [SerializeField] private PoppingNames poppingName = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            PlayerInput player = other.GetComponent<PlayerInput>();
            if (player != null)
            {
                poppingName.TriggerTypeText();
            }
        }
    }
}
