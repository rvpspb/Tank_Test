using UnityEngine;

namespace tank.ui
{
    public abstract class Panel : MonoBehaviour, IPanel
    {       
        public void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        protected virtual void OnShow()
        {

        }

        public void Hide()
        {
            if (enabled)
            {
                gameObject.SetActive(false);
                OnHide();
            }            
        }

        protected virtual void OnHide()
        {

        }
    }
}