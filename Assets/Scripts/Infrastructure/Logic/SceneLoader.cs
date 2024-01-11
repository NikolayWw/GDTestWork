using System;
using System.Collections;

namespace Infrastructure.Logic
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string sceneName, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoaded));

        private static IEnumerator LoadScene(string name, Action onLoaded)
        {
            if (name == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
            {
                onLoaded?.Invoke();
                yield break;
            }

            var waitScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name);
            do
            {
                yield return null;
            } while (waitScene.isDone == false);

            onLoaded?.Invoke();
        }
    }
}