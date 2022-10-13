using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Enemies
{
    [Serializable]
    public class EnemyPath
    {
        public Transform[] PointsToWalk = new Transform[] { };

        public EnemyPath(Transform[] array)
        {
            PointsToWalk = array;
        }
    }
}
