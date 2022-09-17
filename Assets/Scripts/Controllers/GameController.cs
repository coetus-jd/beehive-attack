using Bee.Defenses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private GameObject SwarmOfBeesToAttack;

        [Header("UI")]
        [SerializeField]
        private GameObject PinParent;

        [SerializeField]
        private GameObject Pin;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //RaycastHit hit;
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //if (Physics.Raycast(ray, out hit))
                //{
                //    Debug.Log(hit.transform.gameObject);

                //    //if (hit.transform.name == "$$anonymous$$yObjectName")
                //    //{ print("$$anonymous$$y object is clicked by mouse"); }
                //}

                if (PinParent.transform.childCount > 0)
                    Destroy(PinParent.transform.GetChild(0).gameObject);

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane;
                var position = Camera.main.ScreenToWorldPoint(mousePos);

                var createdPin = Instantiate(Pin, PinParent.transform);

                createdPin.transform.localPosition = position;
            }
        }

        public void Attack()
        {
            SwarmOfBeesToAttack.GetComponent<SwarmOfBees>().Attack();
        }

        public void SetSwarmOfBees(GameObject swarmOfBees)
        {
            SwarmOfBeesToAttack = swarmOfBees;
        }
    }
}
