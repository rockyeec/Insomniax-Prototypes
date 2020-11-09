using UnityEngine;

public class NeighbourRandomizer : MonoBehaviour
{
    [SerializeField] Material[] materials = null;
    [SerializeField] Renderer ren = null;

    private void Awake()
    {
        ren.materials = new Material[1] { materials.GetRandom() };
    }
}
