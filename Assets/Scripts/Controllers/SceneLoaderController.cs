using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Bee.Controllers
{
    public class SceneLoaderController : MonoBehaviour
    {
        public static SceneLoaderController Instance;

        [SerializeField]
        private Image Background;

        [SerializeField]
        private Color Color;

        private float TargetAlpha;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }

        /// <summary>
        /// Carrega uma Scene por seu caminho
        /// </summary>
        /// <param name="sceneName"></param>
        public static void PlayScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        /// <summary>
        /// Carrega uma Scene por seu caminho
        /// </summary>
        /// <param name="sceneName"></param>
        public static void PlayScene(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName, sceneMode);
        }

        /// <summary>
        /// Carrega uma Scene por seu index
        /// </summary>
        /// <param name="sceneIndex"></param>
        public static void PlayScene(int sceneIndex, LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneIndex, sceneMode);
        }

        public static void ReloadCurrentScene()
            => SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

        public async void PlaySceneWithLoading(string sceneName)
        {
            if (Background == null)
                return;

            // TargetAlpha = 0;
            Background.color = new Color(Color.r, Color.g, Color.b, 0);

            var newScene = SceneManager.LoadSceneAsync(sceneName);
            newScene.allowSceneActivation = false;

            Background.enabled = true;
            var fakeTimer = 10f;

            do
            {
                await Task.Delay(100);
                // TargetAlpha = newScene.progress;
                Background.color = new Color(Color.r, Color.g, Color.b, (float)(1 / fakeTimer));

                fakeTimer -= 0.2f;
            } while (fakeTimer > 0);

            newScene.allowSceneActivation = true;
            Background.enabled = false;
        }
    }
}