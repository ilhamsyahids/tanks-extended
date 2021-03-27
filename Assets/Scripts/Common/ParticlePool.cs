using UnityEngine;
using System.Collections.Generic;
using System;

namespace Complete
{
    public class ParticlePool
    {
        int particleAmount;
        Dictionary<int, ParticleSystem[]> poolDictionary = new Dictionary<int, ParticleSystem[]>();

        public ParticlePool(ParticleSystem normalPartPrefab, int amount = 10)
        {
            int poolKey = normalPartPrefab.GetInstanceID();

            if (!poolDictionary.ContainsKey(poolKey))
            {
                poolDictionary.Add(poolKey, new ParticleSystem[amount]);

                for (int i = 0; i < amount; i++)
                {
                    poolDictionary[poolKey][i] = GameObject.Instantiate(normalPartPrefab);
                    poolDictionary[poolKey][i].gameObject.SetActive(false);
                }
            }
        }

        //Returns available GameObject
        public ParticleSystem getAvailabeParticle(ParticleSystem normalPartPrefab)
        {
            int poolKey = normalPartPrefab.GetInstanceID();

            //Get the first GameObject
            ParticleSystem firstObject = poolDictionary[poolKey][0];

            //Move everything Up by one
            shiftUp(poolKey);

            return firstObject;
        }

        //Returns How much GameObject in the Array
        public int getAmount()
        {
            return particleAmount;
        }

        //Moves the GameObject Up by 1 and moves the first one to the last one
        private void shiftUp(int poolKey)
        {
            ParticleSystem[] m_Particles = poolDictionary[poolKey];
            //Get first GameObject
            ParticleSystem firstObject = m_Particles[0];

            firstObject.gameObject.GetComponent<ParticleScript>().CancelInvoke();
            firstObject.gameObject.SetActive(true);
            firstObject.gameObject.GetComponent<ParticleScript>().DeactivateInTime(firstObject.main.duration);

            //Shift the GameObjects Up by 1
            Array.Copy(m_Particles, 1, m_Particles, 0, m_Particles.Length - 1);

            //(First one is left out)Now Put first GameObject to the Last one
            m_Particles[m_Particles.Length - 1] = firstObject;
        }
    }
}