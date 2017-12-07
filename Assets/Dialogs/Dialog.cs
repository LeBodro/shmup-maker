using UnityEngine;

public class Dialog : MonoBehaviour
{
    [System.Serializable]
    public class Replica
    {
        public Actor actor;
        [TextArea] public string text;

        public bool IsLast { get; private set; }

        public void SetLast()
        {
            IsLast = true;
        }
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
