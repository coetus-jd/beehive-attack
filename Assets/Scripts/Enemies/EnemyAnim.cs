using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Enemies
{
    public class EnemyAnim : MonoBehaviour
    {
        private Animator anim;

        void Start() 
        {
            anim = GetComponent<Animator>();    
        }

        public void WalkAnim(Vector2 direction)
        {
            anim.SetFloat("DirX",direction.x);
            anim.SetFloat("DirY",direction.y);
        }
        public void RunningAnim(Vector2 direction)
        {
            anim.SetBool("Run", true);
            anim.SetFloat("DirX",direction.x);
            anim.SetFloat("DirY",direction.y);
        }
    }

}
