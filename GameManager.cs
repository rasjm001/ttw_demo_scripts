using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //handles the logic for the gameplay in each scene including the win conditions etc.

    public GameObject[] bots; // array that will hold gameobject or prefabs of the bots to be used for the Eteam- 
    public float teamA_trench_max_capacity= 20f; // maximun number of bots that can be in player trench at one time ( upgradable)
    public float teamB_trench_max_capacity= 20f; // maximun number of bots that can be in enemy trench at one time ( can increase with game play)
    public float timer; // timer to clear level- may or may not be used depending on gametype
    public int number_of_starting_bots; // number of bots to start in the enemy trench
    public GameObject[] enemy_spawn_locations; // transforms of where enemies can spawn ( with random range either side)
    public float enemy_bot_spawn_rate; //  rate at which enemy infantry spawn in enemy trench (outside of those used in pushes)
    public float allied_soldier_spawn_rate; //  rate at which  infantry spawn in player trench (upgradable)

    public int push_counter = 1; // counts the number of enemy push which have occured
    public float push_timer; //timer which counts down the time between pushes

    //add another array or list here that will be the same length as the bots array- this is the multiplier for the units used in each push for example [5,2,2][bot1,bot2,bot3] would be used to spawn 5 bot1s, and 
    //2 bot2s and bot3s. 

  


    // Start is called before the first frame update
    void Start()
    {
        //spawn enemy bots in the enemy trench (at the spawn locations) - accroding the the number of startings bots variable

        //start the push counter
    }

    // Update is called once per frame
    void Update()
    {
        //spawn infantry in the enemy and player trenches according to the spawn rate at each trench ( units per 10 sec etc) provided the max capacity has not been reached

        //if the push timer reached 0, spawn the number of the bots in the trench using the logic listed above. invoke a reset of the timer after 10 seconds.
        
        //

        
    }
}
