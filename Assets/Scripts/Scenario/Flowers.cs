using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bee.Enums;
using Bee.Defenses;

namespace Bee.Scenario
{
    public class Flowers : MonoBehaviour
    {
        [Header("Particles")]
        [SerializeField]
        private GameObject[] PetalsEffect;

        [Header("Animation")]
        private Animator[] FlowersAnim;


        [Header("Timer")]

        [SerializeField]
        private float ReturnTime = 20f;

        [SerializeField]
        private float StartAnimation = 2f;

        public bool Collect = false;

        private void Start()
        {
            FlowersAnim = GetComponentsInChildren<Animator>();
            for (int i = 0; i < FlowersAnim.Length; i++)
            {
                FlowersAnim[i].SetBool("pollen", true);
            }
            for (int i = 0; i < PetalsEffect.Length; i++)
            {
                PetalsEffect[i].SetActive(false);
            }
        }


        private void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(Tags.Defense))
                return;

            var swarmOfBess = other.GetComponent<SwarmOfBees>();
            
            // If is already attacking or going to attack an enemy, we have to let
            // the swarm pass
            if (swarmOfBess.Attacking || swarmOfBess.TargetTag == Tags.Enemy)
                return;

            if (!Collect)
                StartCoroutine(ReturnPollen());
        }

        public IEnumerator ReturnPollen()
        {
            Collect = true;
            yield return new WaitForSeconds(StartAnimation);

            for (int i = 0; i < FlowersAnim.Length; i++)
            {
                FlowersAnim[i].SetBool("pollen", false);
            }
            for (int i = 0; i < PetalsEffect.Length; i++)
            {
                PetalsEffect[i].SetActive(true);
            }

            yield return new WaitForSeconds(ReturnTime);

            for (int i = 0; i < FlowersAnim.Length; i++)
            {
                FlowersAnim[i].SetBool("pollen", true);
            }
            for (int i = 0; i < PetalsEffect.Length; i++)
            {
                PetalsEffect[i].SetActive(false);
            }

            Collect = false;
        }
    }

}
