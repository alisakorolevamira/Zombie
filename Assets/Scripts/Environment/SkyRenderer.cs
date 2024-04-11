using UnityEngine;

namespace Environment
{
    public class SkyRenderer : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _offset;

        private void Start()
        {
            _renderer.material.mainTextureOffset = new Vector2(_offset, 0);
        }
    }
}