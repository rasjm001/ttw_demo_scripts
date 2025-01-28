using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akila.FPSFramework
{
    [AddComponentMenu("Akila/FPS Framework/Player/Unit Purchase")]
    public class UnitPurchase : MonoBehaviour, IInteractable
    {
        [Tooltip("The name of the interaction to display (e.g., 'Purchase Unit')")]
        public string interactionName = "Purchase Unit";

        [Tooltip("The prefab of the player unit to spawn")]
        public GameObject unitPrefab;

        [Tooltip("The spawn location for the new player unit")]
        public Transform spawnPoint;

        [Tooltip("The cost of the unit in game currency")]
        public int unitCost = 100;

        [Tooltip("Reference to the player's currency system")]
        public CurrencyManager currencyManager;

        public string GetInteractionName()
        {
            return interactionName;
        }

        public void Interact(InteractablesManager source)
        {
            if (currencyManager == null)
            {
                Debug.LogError("CurrencyManager is not assigned!");
                return;
            }

            // Check if the player has enough currency
            if (currencyManager.CurrentBalance >= unitCost)
            {
                // Deduct the cost
                currencyManager.DeductCurrency(unitCost);

                // Spawn the new unit
                if (unitPrefab != null && spawnPoint != null)
                {
                    Debug.Log("spawn Logic goes here");
                    Instantiate(unitPrefab, spawnPoint.position, spawnPoint.rotation);
                    Debug.Log("Unit purchased and spawned successfully!");
                }
                else
                {
                    Debug.LogError("UnitPrefab or SpawnPoint is not assigned!");
                }
            }
            else
            {
                Debug.Log("Not enough currency to purchase this unit.");
            }
        }
    }
}
