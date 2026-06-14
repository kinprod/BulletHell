using Assets.ArchitectureLearn.Scripts.Game.GameRoot.Services;
using Assets.Learn.Scripts.Game.MainScene.Root;
using BaCon;

namespace Assets.BullerHell.Scripts.Game.MainScene
{
    public static class MainSceneRegistrations
    {
        public static void Register(DIContainer container, MainEnterParams mainEnterParams)
        {
            container.RegisterFactory(c => new SomeMainService(c.Resolve<SomeCommonService>()));
        }
    }
}
