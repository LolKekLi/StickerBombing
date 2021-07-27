using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using StikerBombing.UI;

namespace StikerBombing
{
    public class StartupManager : MonoBehaviour
    {
        public const string StartupScene = "Startup";
        public const string TutorialLevel = "TutorialLevel";
        public const string MainUI = "UICommon";

        IEnumerator Start()
        {
            DontDestroyOnLoad(gameObject);

            Init();

            yield return StartCoroutine(LoadScene());

            UISystem.ShowWindow<GameWindow>();

            Destroy(gameObject);
        }

        private void Init()
        {
            AssetsManager.GetInstance();
            //BalanceManager.GetInstance();
            //PoolManager.GetInstance();
        }

        IEnumerator LoadScene()
        {
            yield return SceneManager.LoadSceneAsync(MainUI);

            yield return SceneManager.LoadSceneAsync(TutorialLevel);

            //yield return SceneManager.LoadSceneAsync("EmptyScene");
        }
    }
}