using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] Formation[] formations;

    public bool IsNotDone
    {
        get
        {
            for (int i = 0; i < formations.Length; i++)
                if (formations[i].IsNotDone)
                    return true;
            return false;
        }
    }

    public bool AllShipsAreGone
    {
        get
        {
            for (int i = 0; i < formations.Length; i++)
                if (!formations[i].AllShipsAreGone)
                    return false;
            return true;
        }
    }

    public void Update()
    {
        for (int i = 0; i < formations.Length; i++)
            formations[i].Update();
    }

#if UNITY_EDITOR
    public void RemoveFormation(int index)
    {
        var cache = new Formation[formations.Length - 1];
        for (int i = 0; i < cache.Length; i++)
        {
            if (i < index)
                cache[i] = formations[i];
            else
                cache[i] = formations[i + 1];
        }
        formations = cache;
    }
#endif
}

[System.Serializable]
public class Formation
{
    [SerializeField] NavNode graphStart;
    [SerializeField] Spaceship shipPrefab;
    [SerializeField] int shipCount = 1;
    [SerializeField] float spawnCooldown = 1f;
    Team team = Team.Enemy;

    float secondsToNextSpawn;
    int shipsLeft;

    public bool IsNotDone { get { return shipCount > 0; } }

    public bool AllShipsAreGone { get { return shipCount == 0 && shipsLeft == 0; } }

    public void Update()
    {
        if (shipCount == 0)
            return;
        
        secondsToNextSpawn -= Time.deltaTime;
        if (secondsToNextSpawn <= 0)
        {
            var pilot = CreateShip();
            pilot.StartCourseAt(graphStart);
            secondsToNextSpawn = spawnCooldown;
            shipCount--;
        }
    }

    AutoPilot CreateShip()
    {
        var ship = Object.Instantiate<Spaceship>(shipPrefab, graphStart.Position, Quaternion.Euler(0, 0, 180));
        ship.SetTeam(team);
        shipsLeft++;
        ship.GetComponent<Life>().OnDeath += () => shipsLeft -= 1;

        return ship.gameObject.AddComponent<AutoPilot>();
    }
}
