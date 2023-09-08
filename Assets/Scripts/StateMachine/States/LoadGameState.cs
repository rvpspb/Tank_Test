using npg.states.Infrastructure;
using tank.core;

namespace tank.states
{
	public class LoadGameState : IGameState, IState
	{
		private readonly GameStateMachine _gameStateMachine;		
		private readonly Game _game;

		public LoadGameState(GameStateMachine gameStateMachine, Game game)
		{
			_gameStateMachine = gameStateMachine;
			_game = game;
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
			_game.LoadLevel();	
			_gameStateMachine.Enter<StartGameState>();
		}		
	}
}