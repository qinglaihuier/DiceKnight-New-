using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using InputConfigurationSpace;
namespace System
{
    public interface IInputSystem : ISystem
    {
        public InputConfiguration.GamePlayActions GetGamePlayActions();
    }
    public class InputSystem : AbstractSystem, IInputSystem
    {
        private InputConfiguration inputConfiguration;

        protected override void OnInit()
        {
            inputConfiguration = new InputConfiguration();
            inputConfiguration.Enable();
        }

        public InputConfiguration.GamePlayActions GetGamePlayActions()
        {
            return inputConfiguration.GamePlay;
        }

    }
}
