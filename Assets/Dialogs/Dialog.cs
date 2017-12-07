using UnityEngine;

[CreateAssetMenu]
public class Dialog : ScriptableObject
{
    [System.Serializable]
    public class Replica
    {
        public Actor actor;
        [TextArea] public string text;
    }

    [SerializeField] Replica[] replicas;

    int nextReplicaIndex = 0;

    public bool HasNotEnded
    { 
        get { return nextReplicaIndex < replicas.Length; }
    }

    public void Initialize()
    {
        nextReplicaIndex = 0;
    }

    public Replica GetNextReplica()
    {
        var nextReplica = replicas[nextReplicaIndex];
        nextReplicaIndex++;
        return nextReplica;
    }
}
