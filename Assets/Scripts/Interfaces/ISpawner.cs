using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Interfaces
{
    /// <summary>
    /// Interface with base methods that a spawner should have
    /// </summary>
    public interface ISpawner
    {
        /// <summary>
        /// Spawn a gameobject
        /// </summary>
        void Spawn();

        /// <summary>
        /// Spawn a gameobject given a position
        /// </summary>
        /// <param name="transform"></param>
        void Spawn(Transform transform);

        /// <summary>
        /// Spawn a gameobject that will have another gameobject as a target
        /// </summary>
        /// <param name="target"></param>
        void Spawn(GameObject target);
    }
}
