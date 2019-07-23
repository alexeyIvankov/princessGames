using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy1 : MonoBehaviour

{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}