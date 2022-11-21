using TMPro;
using UnityEngine;

namespace Bee.Controllers
{
    public class PunctuationController : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text BeesCounter; //Number of bees Displayed

        [Header("Game")]
        [SerializeField]
        private int QuantityOfBees = 0; //The current number of bees

        [SerializeField]
        private int BeesQuantityInSwarm = 10_000; //Number used to increment the bees

        public int CurrentQuantityOfBees 
        {
            get 
            {
                return GetCurrentQuantityOfBees();
            }
        }

        void Start() //Number of bees in the start
        {
            ChangeBeesCounterText(QuantityOfBees);
        }

        public void AddSwarm() //add bees to the current number of bees.
        {
            ChangeBeesCounterText(CurrentQuantityOfBees + BeesQuantityInSwarm);
        }

        public void UseSwarm() //Reduce the number of bees when the player attacks
        {
            ChangeBeesCounterText(CurrentQuantityOfBees - BeesQuantityInSwarm);
        }

        public void SetQuantityOfBeesByEnemies(int enemiesQuantity) //Calculate the number of bees with the number of enemis,
        //with that, there will be the exact amount of bees for the number of enemies.
        {
            QuantityOfBees = enemiesQuantity * BeesQuantityInSwarm;

            ChangeBeesCounterText(QuantityOfBees);
        }

        private void ChangeBeesCounterText(int quantity) //Display the number of bees
        {
            BeesCounter.text = quantity.ToString().PadLeft(5, '0');
        }

        private int GetCurrentQuantityOfBees() //Convert to int
            => int.Parse(BeesCounter.text);
    }
}