using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public static event System.Action<bool> OnPause = delegate { };

    [SerializeField] private Button menuButton = null;

    private bool isPause = false;

    private void Awake()
    {
        menuButton.onClick.AddListener(Pause);
    }

    private void Pause()
    {
        isPause = !isPause;
        OnPause(isPause);
        Time.timeScale = (isPause ? 0.0f : 1.0f);
    }
}
