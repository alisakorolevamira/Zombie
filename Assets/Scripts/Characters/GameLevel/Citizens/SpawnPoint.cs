using UnityEngine;

namespace Characters.GameLevel.Citizens
{
    public class SpawnPoint : MonoBehaviour
    {
        public bool IsAvailable;

        private void Start() => IsAvailable = true;
    }
}