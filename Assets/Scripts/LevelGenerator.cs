using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] MapObject[] _templates;
    [SerializeField] private Transform _player;
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _cellSize;

    private HashSet<Vector3Int> _mapMatrix = new HashSet<Vector3Int>();

    private void Update()
    {
        FillRadius(_player.position, _viewRadius);
    }

    private void FillRadius(Vector3 center, float viewRadius)
    {
        var cellCountOnAxis = (int)(viewRadius / _cellSize);
        var fillAreaCenter = WorldToGridPosition(center);

        for (int x = -cellCountOnAxis; x < cellCountOnAxis; x++)
        {
            foreach(MapLayer layer in (MapLayer[])Enum.GetValues(typeof(MapLayer)))
            {
                TryCreateOnLayer(layer, fillAreaCenter + new Vector3Int(x, 0, 0));
            }
        }
    }

    private void TryCreateOnLayer(MapLayer layer, Vector3Int gridPosition)
    {
        gridPosition.y = (int)layer;

        if (_mapMatrix.Contains(gridPosition))
            return;
        else
            _mapMatrix.Add(gridPosition);

        var template = GetRandomTemplate(layer);

        if (template == null)
            return;

        var position = GridToWorldPosition(gridPosition);

        Instantiate(template, position, Quaternion.identity, transform);
    }

    private MapObject GetRandomTemplate(MapLayer layer)
    {
        var variants = _templates.Where(template => template.Layer[0] == layer);

        foreach (var template in variants)
        {
            if (template.Chance > Random.Range(0, 100))
            {
                return template;
            }
        }

        return null;
    }

    private Vector3 GridToWorldPosition(Vector3Int gridPosition)
    {
        return new Vector3(
            gridPosition.x * _cellSize,
            gridPosition.y * _cellSize,
            gridPosition.z * _cellSize);
    }

    private Vector3Int WorldToGridPosition(Vector3 worldPosition)
    {
        return new Vector3Int(
            (int)(worldPosition.x / _cellSize),
            (int)(worldPosition.y / _cellSize),
            (int)(worldPosition.z / _cellSize));
    }
}
