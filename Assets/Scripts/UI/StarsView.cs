using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI
{
    public class StarsView : MonoBehaviour
    {
        private readonly List<Star> _stars = new();

        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _starPrefab;

        public void AddStars(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Star star = Instantiate(_starPrefab, _container).GetComponent<Star>();
                _stars.Add(star);
            }
        }

        public void RemoveAllStars()
        {
            foreach (Star star in _stars)
                Destroy(star.gameObject);

            _stars.Clear();
        }
    }
}
