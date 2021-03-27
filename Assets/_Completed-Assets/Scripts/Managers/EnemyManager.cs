using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Complete
{
    public class EnemyManager : MonoBehaviour
    {
        public LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".
        public ParticleSystem m_ExplosionParticles;         // Reference to the particles that will play on explosion.
        public AudioSource m_ExplosionAudio;                // Reference to the audio that will play on explosion.
        // public Transform m_SpawnPoint;                          // The position and direction the tank will have when it spawns.

        [HideInInspector] public int m_MasterNumber;        // This specifies which player this the manager for.

        public float m_MaxDamage = 100f;                    // The amount of damage done if the explosion is centred on a tank.
        public float m_ExplosionForce = 700f;              // The amount of force added to a tank at the centre of the explosion.
        public float m_MaxLifeTime = 20f;                    // The time in seconds before the shell is removed.
        public float m_ExplosionRadius = 15f;                // The maximum distance away from the explosion tanks can be and are still affected.
        public float lookRadius = 10f;

        private Complete.OfflineTankManager[] tanks;
        private Complete.OfflineGameManager gameManager;
        private Vector3 myPosition;
        NavMeshAgent agent;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
        }
        // Start is called before the first frame update
        void Start()
        {
            //transform.position = m_SpawnPoint.position;
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<Complete.OfflineGameManager>();
            tanks = gameManager.m_Tanks;
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            Vector3 targetDestination = FindClosestEnemy();
            agent.SetDestination(targetDestination);
            if (gameManager.m_RoundWinner != null)
            {
                //Destroy(gameObject);
            }
        }

        private Vector3 FindClosestEnemy()
        {
            Vector3 targetDestination = transform.position;
            myPosition = GetComponent<Transform>().position;

            float closestDistance = Mathf.Infinity;
            float distance;
            //

            foreach (Complete.OfflineTankManager tank in tanks)
            {
                distance = Vector3.Distance(myPosition, tank.m_Instance.transform.position);
                if (distance < closestDistance && tank.m_PlayerNumber != m_MasterNumber)
                {
                    closestDistance = distance;
                    targetDestination = tank.m_Instance.transform.position;
                }
            }
            return targetDestination;
        }

        private float CalculateDamage(Vector3 targetPosition)
        {
            // Create a vector from the shell to the target.
            Vector3 explosionToTarget = targetPosition - transform.position;

            // Calculate the distance from the shell to the target.
            float explosionDistance = explosionToTarget.magnitude;

            // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
            float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

            // Calculate damage as this proportion of the maximum possible damage.
            float damage = relativeDistance * m_MaxDamage;

            // Make sure that the minimum damage is always 0.
            damage = Mathf.Max(0f, damage);

            return damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
            Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

            // Go through all the colliders...
            for (int i = 0; i < colliders.Length; i++)
            {
                // ... and find their rigidbody.
                Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

                // If they don't have a rigidbody, go on to the next collider.
                if (!targetRigidbody)
                    continue;

                // Add an explosion force.
                targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

                // Find the TankHealth script associated with the rigidbody.
                TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

                // If there is no TankHealth script attached to the gameobject, go on to the next collider.
                if (!targetHealth)
                    continue;

                // Calculate the amount of damage the target should take based on it's distance from the shell.
                float damage = CalculateDamage(targetRigidbody.position);

                // Deal this damage to the tank.
                targetHealth.TakeDamage(damage);
            }

            // Unparent the particles from the shell.
            m_ExplosionParticles.transform.parent = null;

            // Play the particle system.
            m_ExplosionParticles.Play();

            // Play the explosion sound effect.
            m_ExplosionAudio.Play();

            // Once the particles have finished, destroy the gameobject they are on.
            ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
            Destroy(m_ExplosionParticles.gameObject, mainModule.duration);

            // Destroy the shell.
            Destroy(gameObject);
        }

    }
}
