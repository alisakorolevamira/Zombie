public sealed class GameSceneManager : SceneManagerBase
{
    public override void InitScenesMap()
    {
        _sceneConfigMap[FirstLevelSceneConfig.Name] = new FirstLevelSceneConfig();
        _sceneConfigMap[SecondLevelSceneConfig.Name] = new SecondLevelSceneConfig();
    }
}
