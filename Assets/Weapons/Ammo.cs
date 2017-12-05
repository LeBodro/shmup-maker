using UnityEngine;

public class Ammo : MonoBehaviour
{
    public void SetOwner(Team owner)
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        // collision should only happen on targetable item
        // if collision matrix and layers are set properly.
        // Attack life directly.
    }
}
