using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akila.FPSFramework
{
 [AddComponentMenu("Akila/FPS Framework/Player/Weapons Bench")]
    public class WeaponsBench : MonoBehaviour, IInteractable
    {
        [Tooltip("The name of the interaction to display (e.g., 'Open Weapons Bench')")]
        public string interactionName = "Open Weapons Bench";

        [Tooltip("Reference to the weapons upgrade canvas")]
        public GameObject weaponsUpgradeCanvas;

        public string GetInteractionName()
        {
            return interactionName;
        }
        
        void Start(){
            weaponsUpgradeCanvas.SetActive(false);
        }

        public void Interact(InteractablesManager source)
        {
            if (weaponsUpgradeCanvas != null)
            {
                bool isActive = weaponsUpgradeCanvas.activeSelf;
                weaponsUpgradeCanvas.SetActive(!isActive);
                Debug.Log("Weapons upgrade GUI opened.");
                
                //implement function to close the gui with key aswell
               
            }
            else
            {
                Debug.LogError("WeaponsUpgradeCanvas is not assigned!");
            }

        }
        
    }}