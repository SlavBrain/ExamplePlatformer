using UnityEngine;

public class MapObject : MonoBehaviour
{
    [SerializeField] private MapLayer[] _layer;
    [SerializeField] private int _chance;

    public MapLayer[] Layer => _layer;

    public int Chance => _chance;

    private void OnValidate()
    {
        _chance = Mathf.Clamp(_chance, 1, 100);
    }
}
