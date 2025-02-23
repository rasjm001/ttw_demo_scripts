using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akila.FPSFramework; 

public class GameManager : MonoBehaviour
{
    [Header("Trench Capacity")]
    public int teamA_trench_max_capacity = 20; // Max bots in player trench
    public int teamB_trench_max_capacity = 20; // Max bots in enemy trench

    [Header("Bot Tracking")]
    public int teamA_bot_counter = 0; // Bots in player trench
    public int teamB_bot_counter = 0; // Bots in enemy trench

    [Header("Spawning Settings")]
    public int number_of_starting_bots = 10; // Initial enemy bots in trench
    public GameObject[] enemy_spawn_locations; // Possible spawn locations
    public GameObject[] bots; // Prefabs of enemy bot types
    public float enemy_bot_spawn_rate = 10f; // Time between spawns in enemy trench
    public float allied_soldier_spawn_rate = 15f; // Time between spawns in player trench

    [Header("Push Mechanics")]
    public int push_counter = 1; // Number of enemy pushes
    public float push_timer = 10f; // Time between pushes
    private float push_timer_reset; // Store initial push timer value

    [Header("Push Scaling")]
    public int[] push_unit_multipliers; // Scaling for push wave size

    [Header("Game Timer")]
    public float timer = 300f; // Timer to clear level

    [Header("Currency")]
    public int startingCurrency = 100;

    void Start()
    {
        push_timer_reset = push_timer; // Store original push time

        // Initialize push multipliers (default: 1 per bot type)
        push_unit_multipliers = new int[bots.Length];
        for (int i = 0; i < push_unit_multipliers.Length; i++)
        {
            push_unit_multipliers[i] = 1; // Start at 1 for each unit type
        }

        // Spawn initial bots in enemy trench
        for (int i = 0; i < number_of_starting_bots; i++)
        {
            SpawnEnemyBot();
        }

        StartCoroutine(SpawnReinforcements());
    }

    void Update()
    {
        // Countdown for the push system
        push_timer -= Time.deltaTime;
        if (push_timer <= 0)
        {
            ExecuteEnemyPush();
            push_timer = push_timer_reset; // Reset push timer
        }

        // Win condition check (placeholder logic)
        if (teamA_bot_counter >= teamA_trench_max_capacity)
        {
            Debug.Log("Player Lost - Too many enemies in the trench!");
        }
        if (teamB_bot_counter == 0 && push_counter > 3)
        {
            Debug.Log("Player Won - Enemy forces depleted!");
        }
    }

    IEnumerator SpawnReinforcements()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemy_bot_spawn_rate);
            if (teamB_bot_counter < teamB_trench_max_capacity)
            {
                SpawnEnemyBot();
            }

            yield return new WaitForSeconds(allied_soldier_spawn_rate);
            if (teamA_bot_counter < teamA_trench_max_capacity)
            {
                SpawnAlliedBot();
            }
        }
    }

    void SpawnEnemyBot()
    {
        if (teamB_bot_counter >= teamB_trench_max_capacity) return; // Prevent overfill

        Transform spawnLocation = enemy_spawn_locations[Random.Range(0, enemy_spawn_locations.Length)].transform;
        GameObject botPrefab = bots[Random.Range(0, bots.Length)];
        Instantiate(botPrefab, spawnLocation.position, Quaternion.identity);
        teamB_bot_counter++;
    }

    void SpawnAlliedBot()
    {
        if (teamA_bot_counter >= teamA_trench_max_capacity) return; // Prevent overfill

        // Similar logic to spawn allied bots if needed
    }

public void ExecuteEnemyPush()
{
    Debug.Log("Enemy Push Started!");

    // Get all active enemy bots in the enemy trench
    GameObject[] enemyBots = GameObject.FindGameObjectsWithTag("Enemy");
    
    foreach (GameObject bot in enemyBots)
    {
        EnemyAI ai = bot.GetComponent<EnemyAI>();
        if (ai != null)
        {
            ai.SetState(EnemyAI.AIState.Pushing); // Correct way to trigger push
        }
    }

    // Scale up push size for the next wave
    for (int i = 0; i < push_unit_multipliers.Length; i++)
    {
        push_unit_multipliers[i] += Random.Range(1, 3); // Increase each type randomly
    }

    push_counter++;
}
 public void subtractBotB()
 {
    teamB_bot_counter -= 1;
 }

}
