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

        public void HeartSetUp(int life)
        {
            for (int i = 0; i < life; i++)
            {
                var Heart = LifeHeart[i].GetComponent<Animator>();
                Heart.SetBool("Damage", false);
            }
        }

        public void HeartControl(int damage, int life)
        {
            if (life <= 0)
            {
                foreach (var heart in LifeHeart)
                {
                    var animator = heart.GetComponent<Animator>();

                    var hasDamage = animator.GetBool("damage");

                    if (hasDamage) continue;

                    animator.SetBool("Damage", true);
                }

                return;
            }

            for (int index = 0; index < damage; index++)
            {
                var Heart = LifeHeart[life + index].GetComponent<Animator>();
                Heart.SetBool("Damage", true);
            }
        }
    }
}
