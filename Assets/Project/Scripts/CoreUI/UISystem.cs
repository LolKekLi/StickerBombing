using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StikerBombing.UI
{
    public class UISystem : MonoBehaviour
    {
        public static Action<Window> OnShown = delegate { };

        private Window[] _windows = null;

        private Window _current = null;
        private Stack<Window> _stack = new Stack<Window>();

        private static UISystem _instance = null;

        [SerializeField]
        private GameObject _windowsContainer = null;

        public Window CurrentWindow
        {
            get => _current;
        }

        private void Awake()
        {
            _instance = this;

            _windows = _windowsContainer.GetComponentsInChildren<Window>(true);
            //_windows.Do(wind => wind.Preload());

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            //_current = GetWindow<MainWindow>();
        }

        public static void ShowWindow<T>()
            where T : Window
        {
            var window = GetWindow<T>();

            if (!ReferenceEquals(_instance._current, null))
            {
                _instance._current.OnHide();
            }

            window.OnShow();

            OnShown(window);

            _instance._current = window;
        }

        private static Window GetWindow<T>()
            where T : Window
        {
            var window = _instance._windows.FirstOrDefault(w => w is T);

            if (!window)
            {
                Debug.LogException(new Exception($"{typeof(T)} not found!"));
            }

            return window;
        }

        public static void ReturnToPreviousWindow()
        {
            if (_instance._stack.Count > 1)
            {
                var prev = _instance._stack.Pop();
            }
            else
            {

            }
        }
    }
}