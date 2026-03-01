using System.Collections.Generic;
using UnityEngine;

namespace AbilityFramework.Runtime
{
    public sealed class AbilityRunner : MonoBehaviour
    {
        [SerializeField] private AbilityDefinition[] abilities;

        private AbilityDefinition PrimaryAbility
        {
            get
            {
                if (abilities == null || abilities.Length == 0)
                {
                    return null;
                }
                
                return abilities[0];
            }
        }

        private readonly Dictionary<AbilityID, float> _cooldowns= new Dictionary<AbilityID, float>();

        void Update()
        {
            if (_cooldowns.Count == 0)
            {
                return;
            }

            List<AbilityID> keys = new List<AbilityID>(_cooldowns.Keys);

            foreach (AbilityID abilityID in keys)
            {
                _cooldowns[abilityID] -= Time.deltaTime;

                if (_cooldowns[abilityID] <= 0f)
                {
                    _cooldowns.Remove(abilityID);
                }
            }
        }

        public void RequestActivate()
        {
            var ability = PrimaryAbility;
            if (ability == null)
            {
                Debug.LogError("AbilityRunner: No AbilityDefinition assigned.");
                return;
            }

            RequestActivate(ability.abilityID);
        }

        public void RequestActivate(AbilityID id)
        {
            var ability = FindAbility(id);
            if (ability == null)
            {
                Debug.LogError($"AbilityRunner: No AbilityDefinition found for {id}.");
                return;
            }

            if (_cooldowns.TryGetValue(id, out var remaining) && remaining > 0f)
            {
                Debug.Log($"{id} on cooldown: {remaining:0.00}s");
                return;
            }

            _cooldowns[id] = ability.cooldownSeconds;
            Debug.Log($"{id} activated. Cooldown: {_cooldowns[id]:0.00}s");
        }

        private AbilityDefinition FindAbility(AbilityID id)
        {
            if (abilities == null) return null;

            for (int i = 0; i < abilities.Length; i++)
            {
                var a = abilities[i];
                if (a != null && a.abilityID == id)
                    return a;
            }

            return null;
        }
    }
}

