using Base.Pattern.Implement;
using System;
namespace Pattern
{
    public class StateMachine
    {
        public State currentState
        {
            get;
            private set;
        }
        public event Action<State> OnStateEnter;
        public event Action<State> OnStateExit;

        ~StateMachine()
        {
            OnStateEnter = null;
            OnStateExit = null;
        }
        public void ChangeState(State newState)
        {
            if (currentState != newState)
            {
                currentState.Exit();
                currentState = newState;
            }
            
            currentState.Enter();
        }

        public void Initialize(State startingState)
        {
            currentState = startingState;
            currentState.Enter();

        }

        public void OnEnter()
        {
            OnStateEnter?.Invoke(currentState);
        }

        public void OnExit()
        {
            OnStateExit?.Invoke(currentState);
        }
    }
}