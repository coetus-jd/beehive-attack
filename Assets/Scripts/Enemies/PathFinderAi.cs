using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Enemies
{
    public class PathFinderAi : MonoBehaviour
    {
        //[Tooltip("Comb Object")]
        //[SerializeField]
        //protected Transform Tartget;

        [Tooltip("All the possibles paths for an enemy follow")]
        [SerializeField]
        protected EnemyPath[] PossiblePaths;

        [Tooltip("Enemy speed")]
        [SerializeField]
        protected float SpeedMove;

        protected Transform[] PathChoosed { get { return PossiblePaths[ChoosenWay].PointsToWalk; } }

        [Tooltip("The choosen way")]
        [SerializeField]
        private int ChoosenWay = 0;

        // private bool Retreat;

        /// <summary>
        /// Index for the waypoint for Enemy walk
        /// </summary>
        private int WayIndex = 0;

        void Start()
        {
            //Enemy start in the first position of the waypoints.
            transform.position = PossiblePaths[ChoosenWay].PointsToWalk[WayIndex].transform.position;
            // Retreat = false;
        }


        void Update()
        {
            Move();
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

            //Verify if it doesn't arrive in the final index    
            if (WayIndex <= PossiblePaths[ChoosenWay].PointsToWalk.Length - 1) // && Retreat == false
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    PossiblePaths[ChoosenWay].PointsToWalk[WayIndex].transform.position,
                    SpeedMove * Time.deltaTime
                );

                if (transform.position == PossiblePaths[ChoosenWay].PointsToWalk[WayIndex].transform.position)
                    WayIndex += 1;

                return;
            }
            
            Destroy(gameObject);
        }
    }
}
