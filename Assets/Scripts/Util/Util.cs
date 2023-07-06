using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static int GetPriority(float[] table)
    {
        float total = 0;
        for (int i = 0; i < table.Length; i++)
        {
            total += table[i];
        }
        float randPoint = Random.value * total;

        for (int i = 0; i < table.Length; i++)
        {
            if (randPoint < table[i])
            {
                return i;
            }
            else
            {
                randPoint -= table[i];
            }
        }
        return -1;
    }
}
