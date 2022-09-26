using Bee.Enums;
using Bee.Interfaces;
using UnityEngine;

namespace Bee.Enemies
{
    public class Person : MonoBehaviour, IEnemy
    {
        [SerializeField]
        private float Velocity = 5f;

        private void Update()
        {
            HandleHorizontalMovement();
        }

        private void HandleHorizontalMovement()
        {
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                return;

            var horizontalMovement = Input.GetAxis("Horizontal");
            var x = (Velocity * horizontalMovement) * Time.deltaTime;
            var y = 0f;

            transform.Translate(new Vector2(x, y));
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(Tags.Base))
            {
                Destroy(gameObject);
                return;
            }
        }

        public Transform[] GetPaths()
        {
            throw new System.NotImplementedException();
        }
    }
}