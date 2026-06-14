using Assets.Learn.Scripts.Game.SimpleScene.Root;
using Assets.Learn.Scripts.Game.GameRoot;
using Assets.Learn.Scripts.Game.MainScene.Root;
using R3;
using System;
using UnityEngine;
using BaCon;
using Assets.BullerHell.Scripts.Game.SimpleScene;
using Assets.BullerHell.Scripts.Game.SimpleScene.Root.View;

public class SimpleSceneEntryPoint : MonoBehaviour
{

    [SerializeField]
    private UISimpleSceneRootBinder _sceneUIRootPrefab;

    public Observable<SimpleExitParams> Run(DIContainer simpleSceneContainer, SimpleEnterParams simpleEnterParams)
    {
        SimpleSceneRegistrations.Register(simpleSceneContainer, simpleEnterParams);
        var gameplayViewModelsContainter = new DIContainer(simpleSceneContainer);
        SimpleViewModelsRegistrations.Register(simpleSceneContainer);

        var uiRoot = simpleSceneContainer.Resolve<UIRootView>();
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);

        Debug.Log($"SIMPLE ENTRY POINT: save file name = {simpleEnterParams.SaveFileName}, lenel to load = {simpleEnterParams.LevelNumber}");


        var mainEnterParams = new MainEnterParams("Fatality");
        var exitParams = new SimpleExitParams(mainEnterParams);
        var exitToMainSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

        return exitToMainSceneSignal;
    }
}
