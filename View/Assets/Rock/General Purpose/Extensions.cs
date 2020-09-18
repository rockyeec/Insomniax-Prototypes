using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T GetRandom<T>(this T[] items)
    {
        return items[Random.Range(0, items.Length)];
    }

    public static T GetRandom<T>(this List<T> items)
    {
        return items[Random.Range(0, items.Count)];
    }

    public static bool IsCloseTo(this Vector3 lhs, in Vector3 rhs, in float minDistance)
    {
        return Mathf.Abs(lhs.x - rhs.x) < minDistance
            && Mathf.Abs(lhs.y - rhs.y) < minDistance
            && Mathf.Abs(lhs.z - rhs.z) < minDistance;
    }
}
