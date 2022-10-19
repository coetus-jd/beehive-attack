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
        GameObject Spawn(Transform parent = null);

        /// <summary>
        /// Spawn a gameobject given a position
        /// </summary>
        /// <param name="transform"></param>
        GameObject Spawn(Transform transform, Transform parent = null);

        /// <summary>
        /// Spawn a gameobject that will have another gameobject as a target
        /// </summary>
        /// <param name="target"></param>
        GameObject Spawn(GameObject target, Transform parent = null);
    }
}
