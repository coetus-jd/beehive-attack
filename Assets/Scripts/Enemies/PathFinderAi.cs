using System.Collections;
using System.Collections.Generic;
using Bee.Enums;
using UnityEngine;
using System.Linq;

namespace Bee.Enemies
{
    [RequireComponent(typeof(EnemyAnim))]
    public class PathFinderAi : MonoBehaviour
    {
        //[Tooltip("Comb Object")]
        //[SerializeField]
        //protected Transform Tartget;

        [Tooltip("All the possibles paths for an enemy follow")]
        [SerializeField]
        protected EnemyPath[] PossiblePaths;

        [SerializeField]
        protected EnemyPath[] FakePaths;

        [Tooltip("Enemy speed")]
        [SerializeField]
        protected float SpeedMove;


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
            ChoosePath();
        }

        private void Move()
        {

            //if (Retreat == true)
            //{
            //    WayIndex = 0;

            //    float randomWay = Random.Range(0, 2);
            //    if (randomWay == 1)
            //        ChoosenWay += 1;

            //    else if (randomWay == 2)
            //        ChoosenWay -= 1;

            //    if (ChoosenWay < 0)
            //        ChoosenWay = PossiblePaths.Length;
            //}

            var hasReached = transform.position == PathChosen[CurrentWayIndex].transform.position;

            if (CurrentWayIndex == 0 && BeingAttacked && hasReached)
            {
                Destroy(gameObject);
                return;
            }

            // Verify if it doesn't arrive in the final index    
            if (hasReached && CurrentWayIndex >= PathChosen.Length - 1) // && Retreat == false
                Destroy(gameObject);

            transform.position = Vector2.MoveTowards(
                transform.position,
                PathChosen[CurrentWayIndex].transform.position,
                SpeedMove * Time.deltaTime
            );

            Dir = ((PathChosen[CurrentWayIndex].transform.position) - transform.position).normalized;
            EnemyAnim.WalkAnim(Dir);

            if (hasReached)
                CurrentWayIndex += BeingAttacked ? -1 : 1;

            return;
        }

        private void LoadAllPossiblePaths()
        {
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

            if (IsFakeEnemy)
            {
                transform.position = FakePaths[ChosenWay].PointsToWalk[CurrentWayIndex].transform.position;
                return;
            }

            transform.position = PossiblePaths[ChosenWay].PointsToWalk[CurrentWayIndex].transform.position;
        }
    }
}
