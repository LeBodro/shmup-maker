using UnityEngine;


public class AIWave : MonoBehaviour
{
    [SerializeField] NavCourse course;
    [SerializeField] Spaceship shipPrefab;
    [SerializeField] int shipCount;
    [SerializeField] float spawnCooldown;
    [SerializeField] Team team = Team.Enemy;

    float secondsToNextSpawn;

    void Update()
    {
        if (shipCount == 0)
            return;
        
        secondsToNextSpawn -= Time.deltaTime;
        if (secondsToNextSpawn <= 0)
        {
            var ship = Instantiate<Spaceship>(shipPrefab, course.GetSpawnPosition(), Quaternion.Euler(0, 0, 180));
            ship.SetTeam(team);
            var pilot = ship.gameObject.AddComponent<AutoPilot>();
            foreach (var step in course.GetSteps())
                pilot.QueueDestination(step);
            secondsToNextSpawn = spawnCooldown;
            shipCount--;
        }
    }
}
