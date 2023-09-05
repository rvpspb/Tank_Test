using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace tank.input
{
    public class KeyboardInput: MonoBehaviour, IInput
    {
        public bool IsFireHold { get; private set; }
        public bool IsInverted { get; private set; }
        public bool IsEnabled { get; private set; }

        private int _directionMod;
        public float Vertical { get; private set; }
        public float Horizontal { get; private set; }

        public event Action OnAnyKey;
        public event Action OnUpdate;        

        private const string s_Vertical = "Vertical";
        private const string s_Horizontal = "Horizontal";
        private const string s_Fire = "Fire1";

        private void Awake()
        {
            SetInverted(false);
            SetEnabled(true);
        }

        private void Update()
        {
            if (!IsEnabled)
            {
                return;
            }

            GetInput();

            
        }

        private void GetInput()
        {
            Vertical = Input.GetAxisRaw(s_Vertical) * GetDirectionMod(IsInverted);
            Horizontal = Input.GetAxisRaw(s_Horizontal) * GetDirectionMod(IsInverted);
            IsFireHold = Input.GetButton(s_Fire);

            if (Input.anyKeyDown)
            {
                OnAnyKey?.Invoke();
            }

            OnUpdate?.Invoke();
        }


        public void SetEnabled(bool value)
        {
            IsEnabled = value;
        }

        public void SetInverted(bool value)
        {
            IsInverted = value;            
        }

        private int GetDirectionMod(bool inverted)
        {
            return inverted ? -1 : 1;
        }
    }
}
