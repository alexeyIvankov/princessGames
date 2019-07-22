using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy1 : MonoBehaviour

{
    public static Transform playerTransform;

    void Awake()
    {
        if (playerTransform = null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(transform.gameObject);
        playerTransform = transform;
    }
}