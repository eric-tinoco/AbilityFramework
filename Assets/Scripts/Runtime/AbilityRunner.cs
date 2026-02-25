using UnityEngine;

namespace AbilityFramework.Runtime
{
    public sealed class AbilityRunner : MonoBehaviour
    {
        [SerializeField] private AbilityDefinition ability;

        private float _cooldownRemaining;

        void Update()
        {
            // Tick cooldown down
            if(_cooldownRemaining > 0)
            {
                _cooldownRemaining -= Time.deltaTime;
                if(_cooldownRemaining < 0 )
                {
                    _cooldownRemaining = 0;
                }
                Debug.Log($"On cooldown remaining: {_cooldownRemaining:0.00}s");
            }
        }

        public void RequestActivate()
        {
            if (ability == null)
            {
                Debug.LogError("AbilityRunner: No AbilityDefinition assigned.");
                return;
            }

            if (_cooldownRemaining > 0f)
            {
                Debug.Log($"On cooldown: {_cooldownRemaining:0.00}s");
                return;
            }

            _cooldownRemaining = ability.cooldownSeconds;
            Debug.Log($"{ability.displayName} activated. Cooldown: {_cooldownRemaining:0.00}s");
        }
    }
}

