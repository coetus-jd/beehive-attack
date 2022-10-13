using Assets.Scripts.Interfaces;
using Bee.Controllers;
using Bee.Enums;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Bee.Defenses
{
    public class SwarmOfBees : MonoBehaviour, IDefense
    {
        [Header("Movement")]
        [SerializeField]
        private float MoveSpeed = 2f;

        [SerializeField]
        private bool IsWalking;

        private ParticleSystem Particles;

        /// <summary>
        /// Number of seconds to await to return to the hive when the swarm
        /// has reached nothing
        /// </summary>
        [SerializeField]
        private float SecondsToReturnToHive = 2f;

        [Header("Position")]
        private float MovementTime;

        [Header("Controllers")]
        private GameController GameController;

        [SerializeField]
        private float Life = 100;

        private GameObject Hive;

        [Header("Enemy")]
        [SerializeField]
        private GameObject TargetToReach;

        void Awake()
        {
            GameController = GameObject.FindGameObjectWithTag(Tags.GameController)
                .GetComponent<GameController>();
            Hive = GameObject.FindGameObjectWithTag(Tags.Hive);
            Particles = GetComponent<ParticleSystem>();
        }

        void Start()
        {
            // When a swarm is created that means that this defense will be used
            // so automatically we use the swarm
            GameController.UseSwarm();
        }

        void Update()
        {
            Attack();
        }

        public void Attack()
        {
            if (TargetToReach == null) return;

            Move();
        }

        public void SetEnemyToAttack(GameObject enemy)
        {
            TargetToReach = enemy;
        }

        public void TakeDamage(float damage)
        {
            Life -= damage;
        }

        private void Move()
        {
            IsWalking = true;

            WalkingParticles();
            IdleParticles();

            transform.position = Vector3.Lerp(
                transform.position,
                TargetToReach.transform.position,
                MovementTime
            );

            MovementTime += 0.02f * Time.deltaTime;

            var hasReachedTarget = Vector3.Distance(transform.position, TargetToReach.transform.position) < 1f;

            if (!hasReachedTarget)
                return;

            IsWalking = false;

            if (TargetToReach.tag == Tags.Hive)
            {
                Destroy(gameObject);

                // If we came back to the hive we will add again the amount
                // that wasn't used
                GameController.AddSwarm();
                return;
            }

            StartCoroutine(ReturnToHive());
        }

        private void IdleParticles()
        {
            if (IsWalking) return;

            var main = Particles.main;
            main.startSize = 0.8f;

            var emission = Particles.emission;
            emission.rateOverTime = 45;

            var shape = Particles.shape;
            shape.radius = 1f;

            var velocityOverLifetime = Particles.velocityOverLifetime;
            velocityOverLifetime.orbitalX = -0.04f;
            velocityOverLifetime.orbitalY = 2.3f;
            velocityOverLifetime.radial = -0.4f;
        }

        private void WalkingParticles()
        {
            if (!IsWalking) return;

            var main = Particles.main;
            main.startSize = 1f;

            var emission = Particles.emission;
            emission.rateOverTime = 50;

            var shape = Particles.shape;
            shape.radius = 1.6f;

            var velocityOverLifetime = Particles.velocityOverLifetime;
            velocityOverLifetime.orbitalX = 0.1f;
            velocityOverLifetime.orbitalY = 1.84f;
            velocityOverLifetime.radial = 0.3f;

        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.gameObject.CompareTag(Tags.Enemy))
                return;
        }

        /// <summary>
        /// Set the defense to return to the hive
        /// </summary>
        /// <returns></returns>
        private IEnumerator ReturnToHive()
        {
            yield return new WaitForSeconds(SecondsToReturnToHive);

            MovementTime = 0f;
            TargetToReach = Hive;
        }
    }
}