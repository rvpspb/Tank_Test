using npg.states.Infrastructure;
using npg.bindlessdi.Contracts;

namespace tank.states
{
	public class StateFactory : IStateFactory
	{
		private readonly IFactory<IExitable> _factory;

		public StateFactory(IFactory<IExitable> factory)
		{
			_factory = factory;
		}

		public TState GetState<TState>() where TState : class, IExitable
		{
			return _factory.Resolve<TState>();
		}
	}
}