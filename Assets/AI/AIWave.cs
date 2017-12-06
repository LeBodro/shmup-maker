using UnityEngine;


public class AIWave : MonoBehaviour
{
    [SerializeField] NavCourse course;
    [SerializeField] AutoPilot shipPrefab;
    [SerializeField] int shipCount;
    [SerializeField] float spawnCooldown;

    float secondsToNextSpawn;

    void Update()
    {
        if (shipCount == 0)
            return;
        
        secondsToNextSpawn -= Time.deltaTime;
        if (secondsToNextSpawn <= 0)
        {
            var pilot = Instantiate<AutoPilot>(shipPrefab, course.GetSpawnPosition(), Quaternion.identity);
            foreach (var step in course.GetSteps())
                pilot.QueueDestination(step);
            secondsToNextSpawn = spawnCooldown;
            shipCount--;
        }
    }
}
