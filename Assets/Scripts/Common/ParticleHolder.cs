using UnityEngine;

namespace Complete
{
    public class ParticleHolder : MonoBehaviour
    {
        public static ParticleHolder m_Instance;
        public ParticleSystem[] effects;
        ParticlePool particlePool;

        public static ParticleHolder instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = FindObjectOfType<ParticleHolder>();
                }
                return m_Instance;
            }
        }

        void Start()
        {
            for (int i = 0; i < effects.Length; i++)
            {
                particlePool = new ParticlePool(effects[i], 5);
            }
        }

        public void playParticle(ParticleSystem particleSystem, Vector3 particlePos)
        {
            ParticleSystem particleToPlay = particlePool.getAvailabeParticle(particleSystem);

            if (particleToPlay != null)
            {
                if (particleToPlay.isPlaying)
                    particleToPlay.Stop();

                particleToPlay.transform.position = particlePos;
                particleToPlay.Play();
            }
        }
    }
}