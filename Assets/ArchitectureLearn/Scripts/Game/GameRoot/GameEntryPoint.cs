using Assets.Learn.Scripts.Game.SimpleScene.Root;
using Assets.Learn.Scripts.Game.GameRoot;
using Assets.Learn.Scripts.Game.MainScene.Root;
using Assets.Learn.Scripts.Utils;
using Scripts.Scenes;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using R3;
using TMPro;
using BaCon;
using Assets.ArchitectureLearn.Scripts.Game.GameRoot.Services;

namespace Scripts.EntryPoint
{

    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private UIRootView _uiRoot;
        private readonly DIContainer _rootContainer = new();
        private DIContainer _cachedSceneContainer;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 120;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);

            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);
            _rootContainer.RegisterInstance(_uiRoot);

            _rootContainer.RegisterFactory(c => new SomeCommonService());
        }

        private void RunGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == ScenesName.MAIN_SCENE)
            {
                _coroutines.StartCoroutine(LoadAndStartMainScene());
                return;
            }

            if (sceneName == ScenesName.SIMPLE_SCENE)
            {
                var enterParams = new SimpleEnterParams(".save", 1);
                _coroutines.StartCoroutine(LoadAndStartSimpleScene(enterParams));
                return;
            }

            if (sceneName != ScenesName.BOOT)
            {
                return;
            }
#endif
            _coroutines.StartCoroutine(LoadAndStartMainScene());
        }

        private IEnumerator LoadAndStartMainScene(MainEnterParams mainEnterParams = null)
        {
            _uiRoot.ShowLoadingScreen();
            _cachedSceneContainer = null;

            yield return LoadScene(ScenesName.BOOT);
            yield return LoadScene(ScenesName.MAIN_SCENE);

            yield return new WaitForSeconds(1);

            var sceneEntryPoint = Object.FindFirstObjectByType<MainSceneEnrtyPoint>();
            var mainSceneContainer = _cachedSceneContainer = new DIContainer(_rootContainer);

            sceneEntryPoint.Run(mainSceneContainer, mainEnterParams).Subscribe(mainExitParams =>
            {
                var targetSceneName = mainExitParams.TargetSceneEnterParams.SceneName;

                if (targetSceneName == ScenesName.SIMPLE_SCENE)
                {
                    _coroutines.StartCoroutine(LoadAndStartSimpleScene(mainExitParams.TargetSceneEnterParams.As<SimpleEnterParams>()));
                }
            });


            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadAndStartSimpleScene(SimpleEnterParams simpleEnterParams)
        {
            _uiRoot.ShowLoadingScreen();
            _cachedSceneContainer = null;

            yield return LoadScene(ScenesName.BOOT);
            yield return LoadScene(ScenesName.SIMPLE_SCENE);

            yield return new WaitForSeconds(1);

            var sceneEntryPoint = Object.FindFirstObjectByType<SimpleSceneEntryPoint>();
            var simpleSceneContainer = _cachedSceneContainer = new DIContainer(_rootContainer);

            sceneEntryPoint.Run(_cachedSceneContainer, simpleEnterParams).Subscribe(simpleExitParams =>
            {
                _coroutines.StartCoroutine(LoadAndStartMainScene(simpleExitParams.MainEnterParams));
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
