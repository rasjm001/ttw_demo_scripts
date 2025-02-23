using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitTrenchVotCounter : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void OnTriggerEnter(Collider other) {


        if(other.CompareTag("Enemy")) // this tag is applied to hip rb of bots
        {
            gameManager.subtractBotB();
           Debug.Log("exict");  
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
