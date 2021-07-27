using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StikerBombing.UI
{
    public class GameWindow : Window
    {
        [Header("Aim Group")]
        [SerializeField]
        private Image _yImage = null;
        [SerializeField]
        private Image _xImage = null;

        [SerializeField]
        private float _speed = 0.1f;

        private Coroutine _aimCor = null;

        public override bool IsPopup
        {
            get => false;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            UIJoystick.Dragged += UIJoystick_Dragged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            UIJoystick.Dragged -= UIJoystick_Dragged;
        }

        public override void OnShow()
        {
            base.OnShow();

            _aimCor = StartCoroutine(AimCor());
        }

        private void UIJoystick_Dragged(Vector3 origin)
        {
            
        }

        private IEnumerator AimCor()
        {
            var transform = _yImage.transform;

            while (true)
            {
                transform.Translate(_speed * Time.deltaTime, 0, 0);

                if (transform.position.x >= 3 || transform.position.x <= -3)
                {
                    _speed *= -1;
                }

                yield return null;
            }
        }
    }
}