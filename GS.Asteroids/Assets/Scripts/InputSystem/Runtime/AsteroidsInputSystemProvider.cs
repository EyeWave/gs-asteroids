using GS.Asteroids.Core.Interfaces;
using System;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

namespace GS.Asteroids.InputSystem
{
    internal sealed class AsteroidsInputSystemProvider : IInputSystem
    {
        public event Action Fire;
        public event Action AlternativeFire;
        public event Action Exit;

        private AsteroidsSimpleControls _controls;

        internal AsteroidsInputSystemProvider()
        {
            _controls = new AsteroidsSimpleControls();

            _controls.Player.Fire.performed += callbackContext =>
            {
                if (callbackContext.interaction is HoldInteraction)
                    AlternativeFire?.Invoke();
                else
                    Fire?.Invoke();
            };

            _controls.UI.Exit.performed += _ =>
            {
                Exit?.Invoke();
            };

            _controls.Enable();
        }

        public Vector2 GetMove()
        {
            return _controls.Player.Move.ReadValue<Vector2>();
        }

        public void Dispose()
        {
            _controls.Disable();
            _controls.Dispose();
        }
    }
}
