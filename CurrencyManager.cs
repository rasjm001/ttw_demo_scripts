using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akila.FPSFramework
{
    [AddComponentMenu("Akila/FPS Framework/Player/Currency Manager")]
    public class CurrencyManager : MonoBehaviour
    {
        [Tooltip("The player's starting currency balance")]
        public int startingBalance = 200;

        private int currentBalance;

        public int CurrentBalance
        {
            get { return currentBalance; }
        }

        private void Start()
        {
            currentBalance = startingBalance;
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
        }
    }
}
