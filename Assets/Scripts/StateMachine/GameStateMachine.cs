using npg.states;

namespace tank.states
{
	public class GameStateMachine : StateMachine<IGameState>
	{
		public GameStateMachine(StateFactory stateFactory) : base(stateFactory)
		{

		}
	}
}