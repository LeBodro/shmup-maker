using UnityEngine;

public class Remains : MonoBehaviour
{
    const float OFFSET_DISTANCE = 5;

    [System.Serializable]
    public class Piece
    {
        public GameObject prefab;
        [Range(0, 1)] public float spawnChance = 1;
        public int maxSpawnCount = 1;
    }

    [SerializeField] Piece[] pieces;

    void Start()
    {
        GetComponent<Life>().OnDeath += SpawnRemains;
    }

    void SpawnRemains()
    {
        foreach (var piece in pieces)
        {
            if (piece.spawnChance >= Random.Range(0f, 1f))
            {
                SpawnPiece(piece);
            }
        }
    }

    void SpawnPiece(Piece piece)
    {
        int count = Random.Range(1, piece.maxSpawnCount + 1);
        for (int i = 0; i < count; i++)
        {
            var spawned = Instantiate(piece.prefab, transform.position, Quaternion.identity);
            Vector3 offset = Random.onUnitSphere * OFFSET_DISTANCE;
            offset.z = 0;
            spawned.transform.position += offset;
        }
    }
}
