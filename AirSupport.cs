using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirSupport : MonoBehaviour
{
    // Positions in the enemy trench
    public Transform transform_one;
    public Transform transform_two;
    public Transform transform_three;
    public Transform transform_four;

    public GameObject mapCanvas;

    public float x_spread;
    public float y_spread;

    // Assign the buttons in the inspector
    public Button tile_1;
    public Button tile_2;
    public Button tile_3;
    public Button tile_4;

    public GameObject missilePrefab;

    // Projectile quantity (upgradeable)
    public int missile_quantity;

    // Cooldown duration before next airstrike
    public float airstrike_cooldown;

    private bool canCallAirstrike = true;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize buttons to inactive state
        SetButtonsInteractable(false);

        // Assign button listeners
        tile_1.onClick.AddListener(() => SelectTile(transform_one));
        tile_2.onClick.AddListener(() => SelectTile(transform_two));
        tile_3.onClick.AddListener(() => SelectTile(transform_three));
        tile_4.onClick.AddListener(() => SelectTile(transform_four));
    }

    // Update is called once per frame
    void Update()
    {
        // Check cooldown and update button interactability
        if (canCallAirstrike)
        {
            SetButtonsInteractable(true);
        }
        else
        {
            SetButtonsInteractable(false);
        }
    }

    void SelectTile(Transform target)
    {
        if (!canCallAirstrike) return;

        StartCoroutine(DeployAirstrike(target));

        // Close map canvas
        mapCanvas.SetActive(false);

        // Reset cooldown
        StartCoroutine(ResetCooldown());
    }

    IEnumerator DeployAirstrike(Transform target)
    {
        // Delay before projectiles drop
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < missile_quantity; i++)
        {
            // Calculate random position within spread range
            float randomX = Random.Range(-x_spread, x_spread);
            float randomY = Random.Range(-y_spread, y_spread);
            Vector3 dropPosition = new Vector3(target.position.x + randomX, target.position.y + randomY, target.position.z);

            // Instantiate missile prefab
            Instantiate(missilePrefab, dropPosition, Quaternion.identity);
        }

        canCallAirstrike = false;
    }

    IEnumerator ResetCooldown()
    {
        // Wait for cooldown duration
        yield return new WaitForSeconds(airstrike_cooldown);

        canCallAirstrike = true;
    }

    void SetButtonsInteractable(bool state)
    {
        tile_1.interactable = state;
        tile_2.interactable = state;
        tile_3.interactable = state;
        tile_4.interactable = state;
    }
}