using System.Collections.Generic;
using UnityEngine;

namespace Bee.Defenses
{
    public class Activator : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> ObjectsToActivate;
        
        [SerializeField]
        private string TagToCompare = "Player";

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag(TagToCompare))
                return;

            if (ObjectsToActivate == null || ObjectsToActivate.Count == 0)
                return;

            foreach (var gameObject in ObjectsToActivate)
                gameObject.SetActive(true);
        }
    }
}