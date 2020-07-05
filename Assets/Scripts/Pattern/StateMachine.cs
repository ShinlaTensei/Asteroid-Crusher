using Pattern.Implement;

namespace Pattern
{
    public class StateMachine
    {
        public State currentState { get; private set; }

        public void ChangeState(State newState)
        {
            currentState.Exit();
            currentState = newState;
            newState.Enter();
        }

        public void Initialize(State startingState)
        {
            currentState = startingState;
            currentState.Enter();
        }
    }
}