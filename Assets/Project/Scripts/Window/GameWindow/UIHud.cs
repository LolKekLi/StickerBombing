using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StikerBombing.UI
{
    public class UIHud : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _hpLabel = null;

        [SerializeField]
        private RectTransform _healthRect = null;

        private RectTransform _transform = null;

        private void Awake()
        {
            _transform = (RectTransform)transform;
        }
    }
}