using Bee.Controllers;
using System.Linq;
using UnityEngine;

namespace Bee.Defenses
{
    public class SwarmOfBees : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private float MoveSpeed = 2f;

        [Header("Position")]
        private Transform[] PathsToEnemy;

        private int CurrentPositionOnPath = 0;

        [Header("Controllers")]
        private GameController GameController;

        private SwarmOfBeesController SwarmOfBeesController;

        void Awake()
        {
            GameController = GameObject.FindGameObjectWithTag("GameController")
                .GetComponent<GameController>();
            SwarmOfBeesController = GameObject.FindGameObjectWithTag("SwarmOfBeesController")
                .GetComponent<SwarmOfBeesController>();
        }

        void Update()
        {
            Move();
        }

        public void SetPathToEnemy(Transform[] paths)
        {
            CurrentPositionOnPath = 0;
            PathsToEnemy = paths.Reverse().ToArray();
        }

        private void Move()
        {
            if (PathsToEnemy == null || PathsToEnemy.Length == 0)
                return;

            // If Enemy didn't reach last waypoint it can move
            // If enemy reached last waypoint then it stops
            if (CurrentPositionOnPath == (PathsToEnemy.Length - 1))
            {
                DestroySwarm();
                return;
            } 

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(
                transform.position,
                PathsToEnemy[CurrentPositionOnPath].transform.position,
                MoveSpeed * Time.deltaTime
            );

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == PathsToEnemy[CurrentPositionOnPath].transform.position)
            {
                CurrentPositionOnPath += 1;
            }
        }

        //public void Attack()
        //{
        //    if (Input.GetMouseButtonDown(0) && Time == 0f)
        //    {
        //        Vector3 mousePos = Input.mousePosition;
        //        mousePos.z = Camera.main.nearClipPlane;
        //        TargetPosition = Camera.main.ScreenToWorldPoint(mousePos);
        //        FinalPosition = TargetPosition.x;
        //    }

        //    if (FinalPosition == 0) return;

        //    // animate the position of the game object...
        //    transform.position = new Vector3(
        //        Mathf.Lerp(InitialPosition, FinalPosition, Time),
        //        gameObject.transform.position.y,
        //        0
        //    );

        //    // .. and increase the t interpolater
        //    Time += 0.5f * UnityEngine.Time.deltaTime;

        //    // now check if the interpolator has reached 1.0
        //    // and swap maximum and minimum so game object moves
        //    // in the opposite direction.
        //    if (Time > 1.0f)
        //    {
        //        Debug.Log("Destination!");

        //        DestroySwarm();
        //        //InitialPosition = FinalPosition;
        //        //FinalPosition = 0f;
        //        //Time = 0f;


        //        // float temp = FinalPosition;
        //        // FinalPosition = InitialPosition;
        //        // InitialPosition = temp;
        //        // Time = 0f;
        //    }
        //}

        private void DestroySwarm()
        {
            SwarmOfBeesController.UseSwarm(100);
            Destroy(gameObject);
        }
    }
}