using Assets.BullerHell.Scripts.Game.GameRoot;
using Scripts.Scenes;

namespace Assets.Learn.Scripts.Game.SimpleScene.Root
{
    public class SimpleEnterParams : SceneEnterParams
    {
        public string SaveFileName { get; private set; }
        public int LevelNumber { get; private set; }

        public SimpleEnterParams(string saveFileName, int levelNumber) : base(ScenesName.SIMPLE_SCENE)
        {
            SaveFileName = saveFileName;
            LevelNumber = levelNumber;
        }
    }
}
