using Assets.ArchitectureLearn.Scripts.Game.GameRoot.Services;
using Assets.Learn.Scripts.Game.SimpleScene.Root;
using BaCon;

namespace Assets.BullerHell.Scripts.Game.SimpleScene
{
    public static class SimpleSceneRegistrations
    {
        public static void Register(DIContainer container, SimpleEnterParams simpleEnterParams)
        {
            container.RegisterFactory(c => new SomeGameplaySevice(c.Resolve<SomeCommonService>()));
        }
    }
}
