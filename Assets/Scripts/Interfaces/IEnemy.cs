using UnityEngine;

namespace Bee.Interfaces
{
    /// <summary>
    /// Interface with base mechanics of an enemy
    /// </summary>
    public interface IEnemy
    {
        /// <summary>
        /// The path that the enemy will make until reach the hive
        /// </summary>
        Transform[] GetPaths();
    }
}
