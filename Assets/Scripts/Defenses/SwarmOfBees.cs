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
        public bool Attacking { get; private set; }

        public string TargetTag
        {
            get
            {
                return TargetToReach?.tag;
            }
        }

        [Header("Movement")]
        [SerializeField]
        private float MoveSpeed = 0.02f;

        [SerializeField]
        private bool IsWalking;

        private bool IsReturningToHive
        {
            get 
            {
                return TargetTag == Tags.Hive;
            }
        }

        private ParticleSystem Particles;

        /// <summary>
        /// Number of seconds to await to return to the hive when the swarm
        /// has reached nothing
        /// </summary>
        [SerializeField]
        private float SecondsToReturnToHive = 2f;

        [Header("Position")]
        [SerializeField]
        private float MovementTime;

        [Header("Controllers")]
        private PunctuationController PunctuationController;

        [SerializeField]
        private float Life = 100;

        [Header("Enemy")]
        private GameObject Hive;

        [SerializeField]
        public GameObject TargetToReach;

        void Awake()
        {
            PunctuationController = GameObject.FindGameObjectWithTag(Tags.PunctuationController)
                .GetComponent<PunctuationController>();
            Hive = GameObject.FindGameObjectWithTag(Tags.Hive);
            Particles = GetComponent<ParticleSystem>();
        }

        void Start()
        {
            // When a swarm is created that means that this defense will be used
            // so automatically we use the swarm
            PunctuationController.UseSwarm();
        }

        void Update()
        {
            Attack();
        }

        public void Attack()
        {
            ReachedEnemy();

            if (TargetToReach == null)
                return;

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

            MovementTime += MoveSpeed * Time.deltaTime;

            var hasReachedTarget = Vector3.Distance(transform.position, TargetToReach.transform.position) < 0.1f;

            if (!hasReachedTarget)
                return;

            // If is attacking or the target is an enemy the defense has to reach the target
            if (Attacking || TargetToReach.tag == Tags.Enemy)
                return;

            IsWalking = false;

            if (TargetToReach.tag == Tags.Hive)
            {
                Destroy(gameObject);

                // If we came back to the hive we will add again the amount
                // that wasn't used
                PunctuationController.AddSwarm();
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
            // If the swarm is returning to the hive and collides with
            // an enemy, it still has to reach the hive
            if (IsReturningToHive)
                return;

            AttackEnemy(collider);
        }

        private void AttackEnemy(Collider2D collider)
        {
            if (!collider.gameObject.CompareTag(Tags.Enemy))
                return;

            Attacking = true;
        }

        private void ReachedEnemy()
        {
            if (Attacking && TargetToReach == null)
                Destroy(gameObject);
        }

        /// <summary>
        /// Set the defense to return to the hive
        /// </summary>
        /// <returns></returns>
        private IEnumerator ReturnToHive()
        {
            TargetToReach = null;
            MovementTime = 0f;

            yield return new WaitForSeconds(SecondsToReturnToHive);

            TargetToReach = Hive;
        }
    }
}