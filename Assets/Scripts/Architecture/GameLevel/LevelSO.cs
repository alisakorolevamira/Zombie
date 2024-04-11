using UnityEngine;

namespace Architecture.GameLevel
{
    [CreateAssetMenu(fileName = "SceneConfig", menuName = "SceneConfig/new Scene")]
    public class LevelSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public int MeduimScore { get; private set; }
        [field: SerializeField] public int HighScore { get; private set; }
        [field: SerializeField] public bool IsAvailable { get; private set; }
        [field: SerializeField] public GameObject Zombie { get; private set; }
    }
}