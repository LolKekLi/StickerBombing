using UnityEngine;

namespace StikerBombing.UI
{
    public abstract class Window : MonoBehaviour
    {
        public abstract bool IsPopup
        {
            get;
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnEnable()
        {
            //User.CurrencyChanged += User_CurrencyChanged;
        }

        protected virtual void OnDisable()
        {
            //User.CurrencyChanged -= User_CurrencyChanged;
        }

        public virtual void Preload()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnShow()
        {
            gameObject.SetActive(true);
        }

        public virtual void Refresh()
        {

        }

        public virtual void OnHide()
        {
            gameObject.SetActive(false);
        }

        private void User_CurrencyChanged()
        {
            Debug.Log($"{nameof(Refresh)}");

            Refresh();
        }
    }
}