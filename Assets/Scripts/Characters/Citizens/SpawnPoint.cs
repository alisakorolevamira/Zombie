using UnityEngine;

namespace Scripts.Characters.Citizens
{
    public class SpawnPoint : MonoBehaviour
    {
        public bool IsAvailable;

        private void Start()
        {
            IsAvailable = true;
        }
    }
}
