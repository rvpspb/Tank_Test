
using System;

namespace tank.core
{
    public interface IDestroy<TType>
    {
        event Action<TType> OnDestroyed;
    }
}
