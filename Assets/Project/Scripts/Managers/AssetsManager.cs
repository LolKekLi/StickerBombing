using UnityEngine;

namespace StikerBombing
{
    public class AssetsManager : GameObjectSingleton<AssetsManager>
    {
        [SerializeField]
        private float _a = 0;

        public float A
        {
            get => _a;
        }
    }
}