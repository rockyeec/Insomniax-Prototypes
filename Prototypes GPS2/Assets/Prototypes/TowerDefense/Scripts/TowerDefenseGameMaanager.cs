using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDefenseGameMaanager : MonoBehaviour
{
    [SerializeField] private Transform routeParent;
    [SerializeField] private GameObject kid = null;


    [SerializeField] private GameObject buttonPrefab = null;
    [SerializeField] private Transform buttonsParent = null;
    [SerializeField] private Transform towersParent = null;

    //------------------------------------------------------------
    // temp
    public KidsRoute route = new KidsRoute();

    [SerializeField] private ButtonScript AddTowersTestButton = null;
    //------------------------------------------------------------

    [System.Serializable]
    public class AvailableTower
    {
        [SerializeField] private GameObject tower = null;
        [SerializeField] private Sprite sprite = null;
        private ButtonScript button = null;
        private Transform towersParent;

        public void AddToAvailable(GameObject buttonPrefab, Transform buttonParent, Transform towersParent)
        {
            this.towersParent = towersParent;
            button = Instantiate(buttonPrefab, buttonParent).GetComponent<ButtonScript>();
            button.GetComponent<Image>().sprite = sprite;
            button.GetComponentInChildren<Text>().text = tower.name;
        }
        public void Tick()
        {
            if (button.IsDown)
            {
                Instantiate(tower, towersParent);
            }
        }
    }

    // add from to-be-available list as player unlocks new towers
    readonly private List<AvailableTower> availableTowers = new List<AvailableTower>();

    // ALL towers ever in this game, assigned in the inspector
    [SerializeField] private List<AvailableTower> toBeAvailableList = new List<AvailableTower>();



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

    float interval = 1.337f;
    float time = 0.0f;

    private void Update()
    {
        foreach (var item in availableTowers)
        {
            item.Tick();
        }


        // temp---------------------------------------------
        if (Time.time >= time)
        {
            if (interval > 0.1337f)
                interval *= 0.5f;
            else
                interval = 6.9f;
            time = Time.time + interval;

            HalloweenKid localKid = Instantiate(kid, route.points[0].position, Quaternion.identity).GetComponent<HalloweenKid>();
            if (localKid != null)
            {
                localKid.Init(route);
            }
        }

        if (AddTowersTestButton.IsDown)
        {
            if (toBeAvailableList.Count != 0)
            {
                availableTowers.Add(toBeAvailableList[0]);
                toBeAvailableList[0].AddToAvailable(buttonPrefab, buttonsParent, towersParent);
                toBeAvailableList.RemoveAt(0);
            }
        }
        //----------------------------------------------------
    }
}
