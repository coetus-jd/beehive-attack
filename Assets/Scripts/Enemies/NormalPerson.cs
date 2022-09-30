using Bee.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Enemies
{
    public class NormalPerson : PathFinderAi, IEnemy
    {
        public Transform[] GetPaths()
            => PathChoosed;
    }

}
