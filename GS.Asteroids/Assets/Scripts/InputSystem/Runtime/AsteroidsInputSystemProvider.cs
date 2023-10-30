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

            _controls.Enable();
        }

        public Vector2 GetMove()
        {
            Vector2 move = _controls.Player.Move.ReadValue<Vector2>();
            return move;
        }

        public void Dispose()
        {
            _controls.Disable();
            _controls.Dispose();
        }
    }
}
