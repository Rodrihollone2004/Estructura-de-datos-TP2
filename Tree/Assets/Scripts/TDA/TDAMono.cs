using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDAMono : MonoBehaviour
{
    [SerializeField] private int[] myArray = { 2, 1, 8, 0, 5, 9, 4, 3 };

    StaticTDASet firstStaticSet;
    StaticTDASet secondStaticSet;

    private void Awake()
    {
        firstStaticSet = new StaticTDASet(5);
        secondStaticSet = new StaticTDASet(5);
    }

    void Start()
    {
        for (int i = 0; i < myArray.Length; i++)
        {
            firstStaticSet.Add(myArray[i]);
            secondStaticSet.Add(myArray[i]);
        }

        firstStaticSet.Remove(8);
        firstStaticSet.Add(4);

        firstStaticSet.Show();
        secondStaticSet.Show();
    }
}
