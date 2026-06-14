using Assets.ArchitectureLearn.Scripts.Game.MainScene.Root.View;
using BaCon;

namespace Assets.BullerHell.Scripts.Game.MainScene.Root.View
{
    public static class MainViewModelsRegistrations
    {
        public static void Register(DIContainer container)
        {
            container.RegisterFactory(c => new UIMainRootViewModel());
        }
    }
}
