using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Delay
{
    static Delay _instance;

    CoroutineRunner coroutineRunner;
    Queue<Coroutine> pending = new Queue<Coroutine>();
    Queue<System.Action> callbacks = new Queue<System.Action>();
    bool running;

    static Delay Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Delay();
            return _instance;
        }
    }

    Delay()
    {
        coroutineRunner = new GameObject("Coroutine Runner").AddComponent<CoroutineRunner>();
    }

    public static Delay Wait(float delay)
    {
        var coroutine = Instance.coroutineRunner.Run(WaitCoroutine(delay));
        
        Instance.pending.Enqueue(coroutine);

        return Instance;
    }

    static IEnumerator WaitCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    public Delay Then(System.Action callback)
    {
        callbacks.Enqueue(callback);
        coroutineRunner.Run(DeferredCall());

        return this;
    }

    IEnumerator DeferredCall()
    {
        if (!running)
        {
            running = true;

            for (; pending.Count > 0;)
                yield return pending.Dequeue();

            for (; callbacks.Count > 0;)
                callbacks.Dequeue()();
            
            running = false;
        }
    }
}
