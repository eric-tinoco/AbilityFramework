using UnityEngine;

namespace AbilityFramework.Runtime
{
    [CreateAssetMenu(menuName = "AbilityFramework/Ability Definition")]
    public class AbilityDefinition : ScriptableObject
    {
        public AbilityID abilityID = AbilityID.Dash;
        [Min(0f)] public float cooldownSeconds = 1f;
    }
}