using System;
using System.Collections.Generic;
using Abstract;

namespace StateMachineComponents
{
    public class StateMachine
    {
        private IState _currentState;
        
        private List<Transition> _currentTrasitions = new List<Transition>();

        private List<Transition> _anyTransitions = new List<Transition>();

        private static List<Transition> EmptyTransitions = new List<Transition>();

        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private class Transition 
        { 
            public Func<bool> Condition { get; }
            public IState To { get; }

            public Transition(Func<bool> condition,IState to)
            {
                Condition = condition;
                To = to;
            }
        }
        public void Setup()
            {
                var transition = GetTransition();
                if (transition != null)
                {
                    SetState(transition.To);
                }
                _currentState?.OnSetup();
            }
            public void SetState(IState state)
            {
                if (state == _currentState)
                    return;
                _currentState?.OnExit();
                _currentState = state;
                _transitions.TryGetValue(_currentState.GetType(),out _currentTrasitions);
                if (_currentTrasitions == null)
                {
                    _currentTrasitions = EmptyTransitions;
                }
                _currentState.OnEnter();
            }
            public void AddTransition(IState from,IState to,Func<bool> predicate)
            {
                if (_transitions.TryGetValue(from.GetType(),out var transitions) == false)
                {
                    transitions = new List<Transition>();
                    _transitions[from.GetType()]=transitions;
                }
                transitions.Add(new Transition(predicate,to));
            }
            public void AddAnyTransition(IState state,Func<bool> predicate)
            {
                _anyTransitions.Add(new Transition(predicate,state));
            }
            private Transition GetTransition()
            {
                foreach (var transition in _anyTransitions)
                {
                    if (transition.Condition())
                    {
                        return transition;
                    }
                }
                foreach (var transition in _currentTrasitions)
                {
                    if (transition.Condition())
                    {
                        return transition;
                    }
                }
                return null;
            }
    }
}