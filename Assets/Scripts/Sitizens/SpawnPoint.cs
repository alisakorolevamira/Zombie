using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool _isAvailable = true;

    public bool IsAvailable { get { return _isAvailable; } }

    public void ChangeAvailability(bool availability)
    {
        _isAvailable = availability;
    }
}
