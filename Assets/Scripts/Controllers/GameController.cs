using Bee.Defenses;
using Bee.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Controllers
{
    public class GameController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private GameObject PinParent;

        [SerializeField]
        private GameObject Pin;

        [Header("Controllers")]
        private DefenseController DefenseController;

        [Header("Enemy")]
        private IEnemy SelectedEnemy;

        void Awake()
        {
            DefenseController = GameObject.FindGameObjectWithTag("DefenseController")
               .GetComponent<DefenseController>();
        }

        void Update()
        {
            HandleGameDefense();
        }

        private void HandleGameDefense()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            CreatePin();
            CreateDefense();
        }

        private void CreatePin()
        {
            if (PinParent.transform.childCount > 0)
                Destroy(PinParent.transform.GetChild(0).gameObject);

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            var position = Camera.main.ScreenToWorldPoint(mousePos);

            var createdPin = Instantiate(Pin, PinParent.transform);

            createdPin.transform.localPosition = position;
        }

        private void CreateDefense()
        {
            if (SelectedEnemy == null)
                return;

            DefenseController.CreateSwarmOfBess(SelectedEnemy.GetPaths());
        }
    }
}
