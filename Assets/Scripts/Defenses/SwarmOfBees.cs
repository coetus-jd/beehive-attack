using Bee.Controllers;
using UnityEngine;

namespace Bee.Defenses
{
    public class SwarmOfBees : MonoBehaviour
    {
        public float InitialPosition;

        public float FinalPosition = 0f;

        private float Time = 0.0f;

        private Vector3 TargetPosition;

        private bool IsSelected;

        private GameController GameController;

        void Awake()
        {
            InitialPosition = gameObject.transform.position.x;
            GameController = GameObject.FindGameObjectWithTag("GameController")
                .GetComponent<GameController>();
        }

        public void Attack()
        {
            if (Input.GetMouseButtonDown(0) && Time == 0f)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane;
                TargetPosition = Camera.main.ScreenToWorldPoint(mousePos);
                FinalPosition = TargetPosition.x;

                Debug.Log(TargetPosition);
            }

            if (FinalPosition == 0) return;

            // animate the position of the game object...
            transform.position = new Vector3(
                Mathf.Lerp(InitialPosition, FinalPosition, Time),
                gameObject.transform.position.y,
                0
            );

            // .. and increase the t interpolater
            Time += 0.5f * UnityEngine.Time.deltaTime;

            // now check if the interpolator has reached 1.0
            // and swap maximum and minimum so game object moves
            // in the opposite direction.
            if (Time > 1.0f)
            {
                Debug.Log("Destination!");
                // Destroy(gameObject);
                InitialPosition = FinalPosition;
                FinalPosition = 0f;
                Time = 0f;
                // float temp = FinalPosition;
                // FinalPosition = InitialPosition;
                // InitialPosition = temp;
                // Time = 0f;
            }
        }

        private void OnMouseDown()
        {
            var color = Color.white;

            IsSelected = !IsSelected;

            if (IsSelected)
                color = Color.grey;

            GetComponent<SpriteRenderer>().color = color;
            GameController.SetSwarmOfBees(gameObject);
        }
    }
}