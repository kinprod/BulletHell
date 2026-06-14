using Assets.Learn.Scripts.Game.GameRoot;
using Assets.Learn.Scripts.Game.SimpleScene.Root;
using BaCon;
using R3;
using System.Collections;
using UnityEngine;

namespace Assets.Learn.Scripts.Game.MainScene.Root
{
    public class MainSceneEnrtyPoint : MonoBehaviour
    {

        [SerializeField]
        private UIMainSceneRootBinder _sceneUIRootPrefab;

        public Observable<MainExitParams> Run(DIContainer mainSceneContainer, MainEnterParams mainEnterParams)
        {
            var uiRoot = mainSceneContainer.Resolve<UIRootView>();
            var uiScene = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSignalSubj = new Subject<Unit>();
            uiScene.Bind(exitSignalSubj);

            Debug.Log($"MAIN MENU ENTRY POINT: Run main scene. Results: {mainEnterParams?.Result}");

            var saveFileName = "o.save";
            var levelNumber = Random.Range(0, 300);
            var simpleEnterParams = new SimpleEnterParams(saveFileName, levelNumber);
            var mainExitParams = new MainExitParams(simpleEnterParams);
            var exitToSimpleSignal = exitSignalSubj.Select(_ => mainExitParams);

            return exitToSimpleSignal;
        }
    }
}