using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bee.Controllers;

namespace Bee.Ui
{
    public class LifeUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] LifeHeart;
        [SerializeField]
        private Sprite LowHeart;
        [SerializeField]
        private Sprite FullHeart;

        public void HeartControl(int life)
        {
                var Heart = LifeHeart[life].GetComponent<Animator>();
                Heart.enabled = true;


        }

    }

}
