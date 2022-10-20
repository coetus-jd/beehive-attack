using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Enemies
{
    public class EnemyAnim : MonoBehaviour
    {
        [SerializeField]
        private Animator anim;

        public void WalkAnim(Vector2 direction)
        {
            anim.SetFloat("DirX",direction.x);
            anim.SetFloat("DirY",direction.y);
            Debug.Log(direction);
        }
        public void RunningAnim(Vector2 direction)
        {
            anim.SetFloat("DirX",direction.x);
            anim.SetFloat("DirY",direction.y);
        }
    }

}
