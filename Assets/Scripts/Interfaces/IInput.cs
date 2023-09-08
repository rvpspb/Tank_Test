using System;

namespace tank.input
{
    public interface IInput 
    {
        float Vertical { get; }
        float Horizontal { get; }
        float WeaponWheel { get; }
        bool IsInverted { get; }
        bool IsEnabled { get; }
        bool IsFireHold { get; }        

        event Action OnAnyKey;
        event Action OnUpdate;
        
        void SetInverted(bool value);
        void SetEnabled(bool value);
    }
}
