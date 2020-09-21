using UnityEngine;

public class AudioManagerInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject audioManagerPrefab = null;
    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            Instantiate(audioManagerPrefab);
        }
    }
}
