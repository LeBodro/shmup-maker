using UnityEngine;

[System.Serializable]
public class AIWave
{
    [SerializeField] NavCourse course;
    [SerializeField] Spaceship shipPrefab;
    [SerializeField] int shipCount;
    [SerializeField] float spawnCooldown;
    [SerializeField] Team team = Team.Enemy;

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
            var ship = Object.Instantiate<Spaceship>(shipPrefab, course.GetSpawnPosition(), Quaternion.Euler(0, 0, 180));
            ship.SetTeam(team);
            shipsLeft++;
            ship.GetComponent<Life>().OnDeath += () => shipsLeft -= 1;
            var pilot = ship.gameObject.AddComponent<AutoPilot>();
            foreach (var step in course.GetSteps())
                pilot.QueueDestination(step);
            secondsToNextSpawn = spawnCooldown;
            shipCount--;
        }
    }
}
