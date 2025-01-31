using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Akila.FPSFramework
{
 [AddComponentMenu("Akila/FPS Framework/Player/Radio")]
    public class Radio : MonoBehaviour, IInteractable
    {

        public FirstPersonController fpsController;


        [Tooltip("The name of the interaction to display (e.g., 'Open Map')")]
        public string interactionName = "Open Map";

        [Tooltip("Reference to the map canvas")]
        public GameObject mapCanvas;

        private bool isPlayerNearby = false;


        public string GetInteractionName()
        {
            return interactionName;
        }

        void Start()
        {
            mapCanvas.SetActive(false);
        }
        public void Interact(InteractablesManager source)
{
    if (mapCanvas != null)
    {
        bool isActive = mapCanvas.activeSelf;
        mapCanvas.SetActive(!isActive);

        if (isActive)
        {
            // Close canvas and lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("Map GUI closed.");
            CloseUI();
        }
        else
        {
            // Open canvas and unlock cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Map GUI opened.");
            OpenUI();
        }
    }
    else
    {
        Debug.LogError("MapCanvas is not assigned!");
    }
}

        private void Update()
{
    if (isPlayerNearby && mapCanvas.activeSelf && Input.GetKeyDown(KeyCode.F))
    {
        mapCanvas.SetActive(false);
        Debug.Log("Map GUI closed because player moved away.");
    }
}
private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
    {
        isPlayerNearby = true;
    }
}

private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        isPlayerNearby = false;
        if (mapCanvas.activeSelf)
        {
            mapCanvas.SetActive(false);
            Debug.Log("Map GUI closed because player moved away.");
            CloseUI();
        }
    }
    }

    public void OpenUI()
{
    fpsController.isUIOpen = true;
    Cursor.lockState = CursorLockMode.None; // Unlock cursor for UI interaction
    Cursor.visible = true;
}

public void CloseUI()
{
    fpsController.isUIOpen = false;
    Cursor.lockState = CursorLockMode.Locked; // Lock cursor back for gameplay
    Cursor.visible = false;
}

public void test_button()
{
    Debug.Log("button has been pressed here");
    mapCanvas.SetActive(false);
    CloseUI();
}
}}
