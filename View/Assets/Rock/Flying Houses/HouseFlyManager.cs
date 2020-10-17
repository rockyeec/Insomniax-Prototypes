using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFlyManager : MonoBehaviour
{
    Vector3 inwards = Vector3.forward;
    List<FlyingHouse> houses = new List<FlyingHouse>();
    bool isGlassesOn = false;
    private void Awake()
    {
        GameScript.OnGlassesOn += GameScript_OnGlassesOn;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;


        Transform[] tempHouses = GetComponentsInChildren<Transform>();
        foreach (var item in tempHouses)
        {
            if (item.name == "Red House" || item.name == "Brown House" || item.name == "Green House")
            {
                item.gameObject.AddComponent<MeshCollider>();
                item.gameObject.AddComponent<BackToOriginal>();

                houses.Add(new FlyingHouse(
                    item.localPosition + Vector3.up * Random.Range(6.9f, 15.0f),
                    item.localPosition, item));
            }
        }
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        isGlassesOn = false;
        foreach (var item in houses)
        {
            item.Reset();
        }
    }

    private void GameScript_OnGlassesOn()
    {
        isGlassesOn = true;
    }

    private void Update()
    {
        if (isGlassesOn)
        {
            for (int i = 0; i < houses.Count; i++)
            {
                houses[i].Animate(inwards);
            }
        }
    }

    public class FlyingHouse
    {
        Vector3 top;
        Vector3 bottom;
        Transform house;

        float elapsed = 0.0f;
        float duration;

        public FlyingHouse(Vector3 top, Vector3 bottom, Transform house)
        {
            this.top = top;
            this.bottom = bottom;
            this.house = house;
            duration = Random.Range(5.2f, 14.5f);
        }

        public void Reset()
        {
            if (bottom.y > top.y)
            {
                Vector3 swap = top;
                top = bottom;
                bottom = swap;
            }
            elapsed = 0.0f;
        }

        public void Animate(Vector3 inwards)
        {
            if (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                house.localPosition = Vector3.LerpUnclamped(
                    bottom.With(z: house.localPosition.z), 
                    top.With(z: house.localPosition.z), 
                    CurveManager.Curve.Evaluate( elapsed / duration));
            }
            else
            {
                elapsed -= duration;
                Vector3 swap = top;
                top = bottom;
                bottom = swap;
            }

            house.localPosition += inwards * Time.deltaTime * 0.3f;
            house.localScale += Vector3.up * 0.2f;
        }
    }
}
