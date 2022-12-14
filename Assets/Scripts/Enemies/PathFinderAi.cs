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
        protected GameController GameController;

        [Tooltip("All the possibles paths for an enemy follow")]
        [SerializeField]
        protected EnemyPath[] PossiblePaths;

        [SerializeField]
        protected EnemyPath[] FakePaths;

        [Tooltip("Enemy speed")]
        [SerializeField]
        protected float SpeedMove;

        [Tooltip("Enemy damage to the hive")]
        [SerializeField]
        public float AttackDamage;

        [Tooltip("Enemy Run")]
        [SerializeField]
        protected float RunningMove;

        protected bool Blinking;

        protected Transform[] PathChosen
        {
            get
            {
                if (FakeEnemy)
                    return FakePaths[ChosenWay].PointsToWalk;

                return PossiblePaths[ChosenWay].PointsToWalk;
            }
        }

        [Tooltip("The chosen way")]
        [SerializeField]
        private int ChosenWay = 0;

        [SerializeField]
        private bool BeingAttacked;

        [SerializeField]
        public bool IsBeingAttacked { get { return BeingAttacked; } }

        /// <summary>
        /// Index for the waypoint for Enemy walk
        /// </summary>
        [SerializeField]
        private int CurrentWayIndex = 0;

        [SerializeField]
        public bool FakeEnemy;

        [SerializeField]
        public bool IsFakeEnemy { get { return FakeEnemy; } }

        private Vector2 Dir;

        private EnemyAnim EnemyAnim;

        /// <summary>
        /// An auxiliary flag that will tell to change the path immediately when
        /// the enemy starts being attacked
        /// </summary>
        private bool UrgentChangePath = false;

        [SerializeField]
        protected float Life = 1;

        void Start()
        {
            DefenseController = GameObject.FindGameObjectWithTag(Tags.DefenseController)
               .GetComponent<DefenseController>();
            GameController = GameObject.FindGameObjectWithTag(Tags.GameController)
               .GetComponent<GameController>();
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
            FakeEnemy = true;
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
            if (defense.TargetToReach.GetInstanceID() != gameObject.GetInstanceID())
                return;

            if (defense.Attacking)
            {
                Life--;
                BeingAttacked = Life <= 0;

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
                if (!IsFakeEnemy)
                {
                    GameController.AddQueenPower(AttackDamage);
                }
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

            var halfWay = PathChosen.Length / 2;

            if (!FakeEnemy && CurrentWayIndex >= halfWay && !Blinking)
                StartCoroutine(BlinkSprite());

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

    /// <summary>
        /// Loading pré-config paths in the game
        /// </summary>
        private void LoadAllPossiblePaths()
        {
            // If was already loaded
            if (PossiblePaths.Length > 0)
                return;

            var allPossiblesPaths = GameObject.FindGameObjectsWithTag(Tags.EnemyPath); //Find the paths

            PossiblePaths = new EnemyPath[allPossiblesPaths.Length]; //Get the paths lenght

            for (int index = 0; index < allPossiblesPaths.Length; index++) //Create the array in array.
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

        /// <summary>
        /// Choose the path where the enemy will go
        /// </summary>
        private void ChoosePath()
        {
            var length = FakeEnemy ? FakePaths.Length - 1 : PossiblePaths.Length - 1; //Define variables to fake and true path.
            var chosen = Random.Range(0, length); // Choose the path.

            ChosenWay = chosen;
            CurrentWayIndex = 0;

            if (FakeEnemy && FakePaths.Length > 0)
            {
                transform.position = FakePaths[ChosenWay].PointsToWalk[CurrentWayIndex].transform.position;
                return;
            }

            if (PossiblePaths.Length < 0)
                return;

            transform.position = PossiblePaths[ChosenWay].PointsToWalk[CurrentWayIndex].transform.position;
        }

        private IEnumerator BlinkSprite()
        {
            Blinking = true;

            var spriteRenderer = GetComponent<SpriteRenderer>();

            var defaultColor = spriteRenderer.color;

            for (int index = 0; index < 10; index++)
            {
                spriteRenderer.color = new Color(
                    defaultColor.r,
                    defaultColor.g,
                    defaultColor.b,
                    index % 2 == 0 ? 1 : 0.5f
                );

                yield return new WaitForSeconds(0.3f);
            }

            spriteRenderer.color = defaultColor;

            // Blinking = false;
        }
    }
}
