using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Scenario
{
    public class Hive : MonoBehaviour
    {
        [SerializeField]
        private Animator anim;


        public void HiveClick()
        {
            anim.SetTrigger("Click");
        }
    }

}
