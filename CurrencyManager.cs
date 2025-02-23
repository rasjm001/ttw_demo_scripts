using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Akila.FPSFramework
{
    [AddComponentMenu("Akila/FPS Framework/Player/Currency Manager")]
    public class CurrencyManager : MonoBehaviour
    {
        private TMP_Text scoreText; // text to assign from inspector for ui to display money

        [Tooltip("The player's starting currency balance")]
        public int startingBalance = 200;

        public int currentBalance;

        public int CurrentBalance
        {
            get { return currentBalance; }
        }

        private void Start()

        {
// Find the GameObject with the "Score" tag and get the TMP_Text component
        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");

        if (scoreObject != null)
        {
            scoreText = scoreObject.GetComponent<TMP_Text>();

            if (scoreText == null)
            {
                Debug.LogError("The object tagged 'Score' does not have a TMP_Text component!");
            }
        }
        else
        {
            Debug.LogError("No GameObject found with the tag 'Score'!");
        }
            currentBalance = startingBalance;
        
        
        scoreText.text = "$: "+ currentBalance.ToString();
        }

        /// <summary>
        /// Deducts the specified amount from the player's balance.
        /// </summary>
        /// <param name="amount">The amount to deduct.</param>
        public void DeductCurrency(int amount)
        {
            if (amount <= 0)
            {
                Debug.LogWarning("Attempted to deduct a non-positive amount.");
                return;
            }

            if (amount > currentBalance)
            {
                Debug.LogError("Insufficient currency to complete the transaction.");
                return;
            }

            currentBalance -= amount;
            Debug.Log($"Currency deducted: {amount}. Current balance: {currentBalance}.");
           scoreText.text = "$: "+ currentBalance.ToString();
        }

        /// <summary>
        /// Adds the specified amount to the player's balance.
        /// </summary>
        /// <param name="amount">The amount to add.</param>
        public void AddCurrency(int amount)
        {
            if (amount <= 0)
            {
                Debug.LogWarning("Attempted to add a non-positive amount.");
                return;
            }

            currentBalance += amount;
            Debug.Log($"Currency added: {amount}. Current balance: {currentBalance}.");
          scoreText.text = "$: "+ currentBalance.ToString();
        }
    }
}
