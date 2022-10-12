using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bee.Defenses
{
    public class SwarmOfBeesParticles : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        private ParticleSystem m_Particles;
        void Start()
        {
            m_Particles = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            WalkingParticles();
            IdleParticles();
        }

        private void FixedUpdate()
        {

        }

        private void IdleParticles()
        {
            if (m_IsWalking) return;

            var main = m_Particles.main;
            main.startSize = 0.8f;

            var emission = m_Particles.emission;
            emission.rateOverTime = 45;

            var shape = m_Particles.shape;
            shape.radius = 1f;

            var velocityOverLifetime = m_Particles.velocityOverLifetime;
            velocityOverLifetime.orbitalX = -0.04f;
            velocityOverLifetime.orbitalY = 2.3f;
            velocityOverLifetime.radial = -0.4f;
        }

        private void WalkingParticles()
        {
            if (!m_IsWalking) return;

            var main = m_Particles.main;
            main.startSize = 1f;

            var emission = m_Particles.emission;
            emission.rateOverTime = 50;

            var shape = m_Particles.shape;
            shape.radius = 1.6f;

            var velocityOverLifetime = m_Particles.velocityOverLifetime;
            velocityOverLifetime.orbitalX = 0.1f;
            velocityOverLifetime.orbitalY = 1.84f;
            velocityOverLifetime.radial = 0.3f;

        }
    }

}
