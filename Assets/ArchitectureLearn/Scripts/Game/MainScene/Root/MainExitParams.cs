using Assets.BullerHell.Scripts.Game.GameRoot;

namespace Assets.Learn.Scripts.Game.MainScene.Root
{
    public class MainExitParams
    {
        public SceneEnterParams TargetSceneEnterParams { get; private set; }

        public MainExitParams(SceneEnterParams sceneEnterParams)
        {
            TargetSceneEnterParams = sceneEnterParams;
        }
    }
}
