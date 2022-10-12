using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// Interface with base mechanics of an defense
    /// </summary>
    interface IDefense
    {
        /// <summary>
        /// Set the enemy to attack
        /// </summary>
        /// <param name="enemy"></param>
        void SetEnemyToAttack(GameObject enemy);
        
        /// <summary>
        /// Attack the defined enemy
        /// </summary>
        void Attack();

        /// <summary>
        /// Make a damage to the defense
        /// </summary>
        void TakeDamage(float damage);
    }
}
