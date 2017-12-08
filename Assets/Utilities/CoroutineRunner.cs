using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    public Coroutine Run(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    public void Stop(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }
}
