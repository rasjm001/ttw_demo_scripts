using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akila.FPSFramework
{
 [AddComponentMenu("Akila/FPS Framework/Player/Purchase Weapons")]
public class PurchaseWeapons : MonoBehaviour, IInteractable
{
    [Tooltip("The name of the interaction to display (e.g., 'Purchase Weapons')")]
        public string interactionName = "Purchase";

        public string weaponName =  "Assult Rifle"; //default name, assign in inspector for each gun

        public string price = "$150"; // assign in inspect for each weapon

        public GameObject weapon; // interactable prefab of weapon that will be obtained

        public GameObject ammo; // instantiate the ammo prefab after player has purchased.

        public Transform weaponSpawnPosition; //where to spawn the prefab upon purchase ( on the table etc)
        public Transform ammospawnposition;


        public string GetInteractionName()
        {
            return interactionName;
        }
    
        public void Interact(InteractablesManager source){

            Debug.Log("interacting with weapon");

            if(weapon != null)
            {
                Instantiate(weapon, weaponSpawnPosition.position, weaponSpawnPosition.rotation);  
                Instantiate(ammo, ammospawnposition.position, ammospawnposition.rotation);        
                Destroy(gameObject);   
            }

            else{
                Debug.Log("no weapon prefab allocated");
            }
        }
}
}