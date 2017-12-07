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

    public Replica GetNextReplica()
    {
        var nextReplica = replicas[nextReplicaIndex];
        nextReplicaIndex++;
        return nextReplica;
    }
}
