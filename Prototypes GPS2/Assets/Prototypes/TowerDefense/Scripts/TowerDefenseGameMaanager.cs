using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDefenseGameMaanager : MonoBehaviour
{
    [SerializeField] private Transform routeParent;
    [SerializeField] private GameObject kid = null;
    [SerializeField] private Button tower1Button = null;
    [SerializeField] private GameObject tower = null;
    [SerializeField] private Transform towersParent = null;

    // temp
    public KidsRoute route = new KidsRoute();

    private void Awake()
    {        
        Transform[] points = routeParent.GetComponentsInChildren<Transform>();
        List<Transform> pointList = new List<Transform>();
        foreach (var item in points)
        {
            pointList.Add(item);
        }
        if (pointList.Contains(routeParent))
            pointList.Remove(routeParent);
        route.points = pointList.ToArray();
    }

    private void Start()
    {
        tower1Button.onClick.AddListener(CreateTower1);
    }

    void CreateTower1()
    {
        Instantiate(tower, towersParent);
    }

    float interval = 0.69f;
    float time = 0.0f;

    private void Update()
    {
        if (Time.time >= time)
        {
            time = Time.time + interval;

            HalloweenKid localKid = Instantiate(kid, route.points[0].position, Quaternion.identity).GetComponent<HalloweenKid>();
            if (localKid != null)
            {
                localKid.Init(route);
            }
        }
    }
}
