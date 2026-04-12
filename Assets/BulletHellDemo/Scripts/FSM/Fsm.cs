using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fsm
{
    public class Fsm
    {
        private FsmState _currentState { get; set; }
        private Dictionary<Type, FsmState> _states = new Dictionary<Type, FsmState>();

        public void AddState(FsmState newState)
        {
            _states.Add(newState.GetType(), newState);
        }

        public void SetState<T>() where T : FsmState
        {
            Type type = typeof(T);

            if (_currentState != null && _currentState.GetType() == type)
            {
                return;
            }

            if (_states.TryGetValue(type, out var state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState?.Enter();
            }
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
    }
}