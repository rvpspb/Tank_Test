using npg.states.Infrastructure;
using Cysharp.Threading.Tasks;
using npg.bindlessdi.UnityLayer;
using tank.core;
using tank.config;
using tank.input;
using tank.ui;

namespace tank.states
{
	public class EndGameState : IGameState, IPayloadedState<PaddleSide>
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly UnityObjectContainer _unityObjectContainer;
		private readonly GameConfig _gameConfig;
		private readonly GameController _gameController;
		private readonly IInput _input;
		private ResultPanel _resultPanel;

		public EndGameState(GameStateMachine gameStateMachine, UnityObjectContainer unityObjectContainer, GameConfig gameConfig, GameController gameController, IInput input)
		{
			_gameStateMachine = gameStateMachine;
			_unityObjectContainer = unityObjectContainer;
			_gameConfig = gameConfig;
			_gameController = gameController;
			_input = input;
		}

		public void Enter(PaddleSide paddleSide)
		{
			if (!_unityObjectContainer.TryGetObject(out _resultPanel))
			{
				return;
			}

			_resultPanel.SetWinner(paddleSide);
			_resultPanel.Show();

			_input.OnAnyKey += RestartGame;
		}

		public void Exit()
		{
			_input.OnAnyKey -= RestartGame;

			_gameController.EndGame();
			_resultPanel.Hide();
		}
				
		private void RestartGame()
		{
			_gameStateMachine.Enter<StartGameState>();
		}
	}
}