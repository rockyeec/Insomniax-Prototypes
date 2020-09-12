using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDefenseGameMaanager : MonoBehaviour
{
    [SerializeField] private Button tower1Button;
    [SerializeField] private GameObject tower;
    [SerializeField] private Transform towersParent;

    // temp
    public KidsRoute route = new KidsRoute();

    private void Start()
    {
        tower1Button.onClick.AddListener(CreateTower1);
    }

    void CreateTower1()
    {
        Instantiate(tower, towersParent);
    }
}
