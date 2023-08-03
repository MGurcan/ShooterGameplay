using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private static bool isCreated = false;

    private void Awake()
    {
        if (!isCreated)
        {
            DontDestroyOnLoad(this.gameObject);
            isCreated = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
