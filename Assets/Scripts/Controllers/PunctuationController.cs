using TMPro;
using UnityEngine;

namespace Bee.Controllers
{
    public class PunctuationController : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text BeesCounter;

        [Header("Game")]
        [SerializeField]
        private int QuantityOfBees = 0;

        [SerializeField]
        private int BeesQuantityInSwarm = 10_000;

        public int CurrentQuantityOfBees 
        {
            get 
            {
                return GetCurrentQuantityOfBees();
            }
        }

        void Awake()
        {
            ChangeBeesCounterText(QuantityOfBees);
        }

        public void AddSwarm()
        {
            ChangeBeesCounterText(CurrentQuantityOfBees + BeesQuantityInSwarm);
        }

        public void UseSwarm()
        {
            ChangeBeesCounterText(CurrentQuantityOfBees - BeesQuantityInSwarm);
        }

        private void ChangeBeesCounterText(int quantity)
        {
            BeesCounter.text = quantity.ToString().PadLeft(5, '0');
        }

        private int GetCurrentQuantityOfBees()
            => int.Parse(BeesCounter.text);
    }
}