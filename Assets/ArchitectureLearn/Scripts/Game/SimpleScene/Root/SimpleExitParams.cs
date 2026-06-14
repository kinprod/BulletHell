

using Assets.Learn.Scripts.Game.MainScene.Root;

namespace Assets.Learn.Scripts.Game.SimpleScene.Root
{
    public class SimpleExitParams
    {
        public MainEnterParams MainEnterParams { get; private set; }

        public SimpleExitParams(MainEnterParams mainEnterParams)
        {
            MainEnterParams = mainEnterParams;
        }
    }
}
