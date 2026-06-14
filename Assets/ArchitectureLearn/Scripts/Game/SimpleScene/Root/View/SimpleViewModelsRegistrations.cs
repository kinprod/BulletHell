using Assets.ArchitectureLearn.Scripts.Game.SimpleScene.Root.View;
using BaCon;

namespace Assets.BullerHell.Scripts.Game.SimpleScene.Root.View
{
    public static class SimpleViewModelsRegistrations
    {
        public static void Register(DIContainer container)
        {
            container.RegisterFactory(c => new UISimpleRootViewModel(c.Resolve<SomeGameplaySevice>()));
            container.RegisterFactory(c => new WorldGameplayRootViewModel());
        }
    }
}
