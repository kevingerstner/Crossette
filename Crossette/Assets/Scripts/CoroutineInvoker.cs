using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineInvoker : MonoBehaviour
{
    // Singleton instance of the CoroutineInvoker
    private static CoroutineInvoker instance;

    private void Awake()
    {
        // Ensure there is only one instance of CoroutineInvoker
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to invoke a coroutine from any class
    public static void InvokeCoroutine(IEnumerator coroutine)
    {
        instance.StartCoroutine(coroutine);
    }
}
