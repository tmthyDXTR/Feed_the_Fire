using UnityEngine;

namespace SimpleFogOfWar
{
    public class FogOfWarInfluence : MonoBehaviour
    {

        /// <summary>
        /// Uncovered radius around the entity
        /// </summary>
        public float ViewDistance;
        public float minFlicker;
        public float maxFlicker;
        /// <summary>
        /// Suspends the fog influence for the entity
        /// </summary>
        public bool Suspended;

        void Awake()
        {
            FogOfWarSystem.RegisterInfluence(this);
            minFlicker = ViewDistance * 0.98f;
            maxFlicker = ViewDistance * 1.02f;
        }

        void Update()
        {
           // ViewDistance = Random.Range(minFlicker, maxFlicker);
        }

        void OnDestroy()
        {
            FogOfWarSystem.UnregisterInfluence(this);
        }

    }
}
