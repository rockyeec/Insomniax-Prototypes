using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [SerializeField] private Material wallMaterial = null;
    [SerializeField] private Material fadedOutMaterial = null;

    private static MaterialManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    public static Material FadedOutMaterial { get { return instance.fadedOutMaterial; } }
    public static Material WallMaterial { get { return instance.wallMaterial; } }
}
