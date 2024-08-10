using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FractaExtensions
{
    public static T GetRandomItem<T>(this List<T> list)
    {
        var rdm = Random.Range(0, list.Count);
        return list[rdm];
    }

    public static string ReplaceAt(this string input, int index, char newChar)
    {
        if (index < 0 || index >= input.Length)
        {
            return input;
        }

        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }
}
