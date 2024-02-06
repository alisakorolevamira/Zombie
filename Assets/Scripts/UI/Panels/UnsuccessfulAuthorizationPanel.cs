using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Panels
{
    public class UnsuccessfulAuthorizationPanel : Panel
    {
        [SerializeField] private Button _closeButton;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Close);
        }
    }
}
