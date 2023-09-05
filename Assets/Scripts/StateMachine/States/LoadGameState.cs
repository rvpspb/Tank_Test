using npg.states.Infrastructure;
using tank.core;

namespace tank.states
{
	public class LoadGameState : IGameState, IState
	{
		private readonly GameStateMachine _gameStateMachine;		
		private readonly GameController _gameController;

		public LoadGameState(GameStateMachine gameStateMachine, GameController gameController)
		{
			_gameStateMachine = gameStateMachine;			
			_gameController = gameController;
		}

		public void Enter()
		{
			Load();			
		}

		public void Exit()
		{

		}

		private void Load()
		{	 
			_gameController.LoadLevel();	
			_gameStateMachine.Enter<StartGameState>();
		}		
	}
}