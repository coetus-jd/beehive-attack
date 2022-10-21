using System.Collections;
using System.Collections.Generic;
using Bee.Enums;
using UnityEngine;
using System.Linq;
using Bee.Controllers;
using Bee.Defenses;

namespace Bee.Enemies
{
    [RequireComponent(typeof(EnemyAnim))]
    public abstract class PathFinderAi : MonoBehaviour
    {
        [Header("Controllers")]
        protected DefenseController DefenseController;

        [Tooltip("All the possibles paths for an enemy follow")]
        [SerializeField]
        protected EnemyPath[] PossiblePaths;

        [SerializeField]
        protected EnemyPath[] FakePaths;

        [Tooltip("Enemy speed")]
        [SerializeField]
        protected float SpeedMove;

        [Tooltip("Enemy Run")]
        [SerializeField]
        protected float RunningMove;

        protected Transform[] PathChosen
        {
            get
            {
                if (IsFakeEnemy)
                    return FakePaths[ChosenWay].PointsToWalk;

                return PossiblePaths[ChosenWay].PointsToWalk;
            }
        }

        [Tooltip("The chosen way")]
        [SerializeField]
        private int ChosenWay = 0;

        [SerializeField]
        protected bool BeingAttacked;

        /// <summary>
        /// Index for the waypoint for Enemy walk
        /// </summary>
        [SerializeField]
        private int CurrentWayIndex = 0;

        [SerializeField]
        private bool IsFakeEnemy;

        private Vector2 Dir;

        private EnemyAnim EnemyAnim;

        /// <summary>
        /// An auxiliary flag that will tell to change the path immediately when
        /// the enemy starts being attacked
        /// </summary>
        private bool UrgentChangePath = false;

        [SerializeField]
        protected float Life = 1;

        void Awake()
        {
            DefenseController = GameObject.FindGameObjectWithTag(Tags.DefenseController)
               .GetComponent<DefenseController>();
        }

        void Start()
        {
            EnemyAnim = GetComponent<EnemyAnim>();
            LoadAllPossiblePaths();
            LoadFakePaths();
            ChoosePath();
        }

        void Update()
        {
            if (PathChosen == null || PathChosen.Length == 0)
                return;

            Move();
        }

        public void SetAsFakeEnemy()
        {
            IsFakeEnemy = true;
            CurrentWayIndex = 0;
            LoadFakePaths();
            ChoosePath();
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.gameObject.CompareTag(Tags.Defense))
                return;

            var defense = collider.gameObject.GetComponent<SwarmOfBees>();

            // If already is attacking something and accidentally collides with another
            // enemy then we should guarantee that we attacking the same enemy by his ID
            if (defense.Attacking && defense.TargetToReach.GetInstanceID() == gameObject.GetInstanceID())
            {
                Life--;
                BeingAttacked = Life == 0;

                // If the enemy have more life then more defenses will be needed
                // so we clean the current defense in order to not accumulate
                if (!BeingAttacked)
                    Destroy(collider.gameObject, 1.2f);
            }
        }

        private void Move()
        {
            var hasReached = transform.position == PathChosen[CurrentWayIndex].transform.position;

            if (CurrentWayIndex == 0 && BeingAttacked && hasReached)
            {
                Destroy(gameObject);
                return;
            }

            if (!BeingAttacked)
                EnemyAnim?.WalkAnim(Dir);
            else
            {
                SpeedMove = RunningMove;
                EnemyAnim?.RunningAnim(Dir);
            }

            // Verify if it doesn't arrive in the final index    
            if (hasReached && CurrentWayIndex >= PathChosen.Length - 1)
                Destroy(gameObject);

            transform.position = Vector2.MoveTowards(
                transform.position,
                PathChosen[CurrentWayIndex].transform.position,
                SpeedMove * Time.deltaTime
            );

            Dir = ((PathChosen[CurrentWayIndex].transform.position) - transform.position).normalized;

            if (BeingAttacked && !UrgentChangePath)
            {
                UrgentChangePath = true;
                CurrentWayIndex -= 1;
                return;
            }

            if (hasReached)
                CurrentWayIndex += BeingAttacked ? -1 : 1;
        }

        private void LoadAllPossiblePaths()
        {
            // If was already loaded
            if (PossiblePaths.Length > 0)
                return;

            var allPossiblesPaths = GameObject.FindGameObjectsWithTag(Tags.EnemyPath);

            PossiblePaths = new EnemyPath[allPossiblesPaths.Length];

            for (int index = 0; index < allPossiblesPaths.Length; index++)
            {
                var currentPath = allPossiblesPaths[index];

                var allPointsToWalk = currentPath.GetComponentsInChildren<Transform>();

                // Skip 1 because the first game object is the parent itself
                allPointsToWalk = allPointsToWalk.Skip(1).ToArray();

                PossiblePaths[index] = new EnemyPath(allPointsToWalk);
            }
        }

        private void LoadFakePaths()
        {
            // If was already loaded
            if (FakePaths.Length > 0)
                return;

            var allFakesPaths = GameObject.FindGameObjectsWithTag(Tags.EnemyFakePath);

            FakePaths = new EnemyPath[allFakesPaths.Length];

            for (int index = 0; index < allFakesPaths.Length; index++)
            {
                var currentPath = allFakesPaths[index];

                var allPointsToWalk = currentPath.GetComponentsInChildren<Transform>();

                // Skip 1 because the first game object is the parent itself
                allPointsToWalk = allPointsToWalk.Skip(1).ToArray();

                FakePaths[index] = new EnemyPath(allPointsToWalk);
            }
        }

        private void ChoosePath()
        {
            var length = IsFakeEnemy ? FakePaths.Length - 1 : PossiblePaths.Length - 1;
            var chosen = Random.Range(0, length);

            ChosenWay = chosen;
            CurrentWayIndex = 0;

            if (IsFakeEnemy && FakePaths.Length > 0)
            {
                transform.position = FakePaths[ChosenWay].PointsToWalk[CurrentWayIndex].transform.position;
                return;
            }

            if (PossiblePaths.Length < 0)
                return;

            transform.position = PossiblePaths[ChosenWay].PointsToWalk[CurrentWayIndex].transform.position;
        }
    }
}
