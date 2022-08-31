using System.Collections.Generic;
using Datas.ValueObject;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Particle", menuName = "ColorRunners/CD_Particle", order = 0)]
    public class CD_Particle : ScriptableObject
    {
        public ParticleData ParticleData;
    }
}