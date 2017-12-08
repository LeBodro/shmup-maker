using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Promise
{
    Queue<IEnumerator> pending = new Queue<IEnumerator>();
    bool running;

    static CoroutineRunner _runner;

    static CoroutineRunner Runner
    {
        get
        {
            if (_runner == null)
                _runner = new GameObject("Coroutine Runner").AddComponent<CoroutineRunner>();
            return _runner;
        }
    }

    public Promise Wait(float delay)
    {
        pending.Enqueue(WaitCoroutine(delay));
        return this;
    }

    // Fix this to avoid calling it in "Wait"
    static IEnumerator WaitCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    public Promise Then(System.Action callback)
    {
        Then(ToCoroutine(callback));
        return this;
    }

    IEnumerator ToCoroutine(System.Action callback)
    {
        callback();
        yield return null;
    }

    public Promise Then(IEnumerator promise)
    {
        pending.Enqueue(promise);
        if (!running)
            Runner.Run(RunQueue());
        return this;
    }

    IEnumerator RunQueue()
    {
        running = true;

        for (; pending.Count > 0;)
            yield return Runner.Run(pending.Dequeue());
        
        running = false;
    }
}
