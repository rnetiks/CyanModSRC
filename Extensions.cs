using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Extensions
{
    public static bool AlmostEquals(this float target, float second, float floatDiff)
    {
        return (Mathf.Abs((float) (target - second)) < floatDiff);
    }

    public static bool AlmostEquals(this Quaternion target, Quaternion second, float maxAngle)
    {
        return (Quaternion.Angle(target, second) < maxAngle);
    }

    public static bool AlmostEquals(this Vector2 target, Vector2 second, float sqrMagnitudePrecision)
    {
        Vector2 vector = target - second;
        return (vector.sqrMagnitude < sqrMagnitudePrecision);
    }

    public static bool AlmostEquals(this Vector3 target, Vector3 second, float sqrMagnitudePrecision)
    {
        Vector3 vector = target - second;
        return (vector.sqrMagnitude < sqrMagnitudePrecision);
    }

    public static bool Contains(this int[] target, int nr)
    {
        if (target != null)
        {
            for (int i = 0; i < target.Length; i++)
            {
                if (target[i] == nr)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static string Cut(this string content)
    {
        return content.Substring(content.IndexOf(' ') + 1);
    }

    public static PhotonView GetPhotonView(this GameObject go)
    {
        return go.GetComponent<PhotonView>();
    }

    public static PhotonView[] GetPhotonViewsInChildren(this GameObject go)
    {
        return go.GetComponentsInChildren<PhotonView>(true);
    }

    public static void Merge(this IDictionary target, IDictionary addHash)
    {
        if ((addHash != null) && !target.Equals(addHash))
        {
            foreach (object obj2 in addHash.Keys)
            {
                target[obj2] = addHash[obj2];
            }
        }
    }

    public static void MergeStringKeys(this IDictionary target, IDictionary addHash)
    {
        if ((addHash != null) && !target.Equals(addHash))
        {
            foreach (object obj2 in addHash.Keys)
            {
                if (obj2 is string)
                {
                    target[obj2] = addHash[obj2];
                }
            }
        }
    }

 

    public static void StripKeysWithNullValues(this IDictionary original)
    {
        object[] objArray = new object[original.Count];
        int num = 0;
        foreach (object obj2 in original.Keys)
        {
            objArray[num++] = obj2;
        }
        for (int i = 0; i < objArray.Length; i++)
        {
            object key = objArray[i];
            if (original[key] == null)
            {
                original.Remove(key);
            }
        }
    }

    public static ExitGames.Client.Photon.Hashtable StripToStringKeys(this IDictionary original)
    {
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        IDictionaryEnumerator enumerator = original.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                DictionaryEntry current = (DictionaryEntry)enumerator.Current;
                if (current.Key is string)
                {
                    hashtable[current.Key] = current.Value;
                }
            }
        }
        finally
        {
            IDisposable disposable = enumerator as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
        return hashtable;
    }

   

    public static string ToStringFull(this IDictionary origin)
    {
        return SupportClass.DictionaryToString(origin, false);
    }
}

