using UnityEngine;

public class DontDestroyOnLoadInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject audioManagerPrefab = null;
    [SerializeField] private GameObject diaryPrefab = null;

    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            Instantiate(audioManagerPrefab);
        }
        if(DontDestroyScript.Instance == null)
        {
            Instantiate(diaryPrefab);
        }
        Destroy(gameObject);
    }
}
