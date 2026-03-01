using UnityEngine;
using UnityEngine.InputSystem;

namespace AbilityFramework.Runtime
{
    public sealed class PlayerAbilityInput : MonoBehaviour
    {
        [SerializeField] private AbilityRunner runner;

        private void Awake()
        {
            // Convenience: auto-wire if on same GameObject
            if (runner == null)
            {
                runner = GetComponent<AbilityRunner>();
            }
        }

        // Called by PlayerInput (Send Messages) when Ability1 triggers
        public void OnAbility1()
        {
            if (runner == null)
            {
                return;
            }

            runner.RequestActivate(AbilityID.Dash);
        }
    }
}


