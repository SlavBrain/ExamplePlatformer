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
    [SerializeField] private TemplatesSpawner[] _variations;

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
            foreach (MapLayer layer in (MapLayer[])Enum.GetValues(typeof(MapLayer)))
            {
                TryCreateOnLayer(layer, fillAreaCenter + new Vector3Int(x, 0, 0));
            }
        }
    }

    private void TryCreateOnLayer(MapLayer layer, Vector3Int gridPosition)
    {
        gridPosition.y = (int)layer;

        if (!TryAddNewPositionInGrid(gridPosition))
        {
            return;
        }
        else
        {
            var template = GetRandomTemplateSpawner(layer);

            if (template == null)
                return;

            template.Spawn(gridPosition, this);
        }

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

    private TemplatesSpawner GetRandomTemplateSpawner(MapLayer layer)
    {
        var variants = _variations.Where(template => template.Template.Layer.Where(availableLayer=> availableLayer == layer).ToList().Count!=0);

        foreach (var variant in variants)
        {
            if (variant.Template.Chance > Random.Range(0, 100))
            {
                return variant;
            }
        }

        return null;
    }

    public Vector3 GridToWorldPosition(Vector3Int gridPosition)
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

    public bool TryAddNewPositionInGrid(Vector3Int gridPosition)
    {
       return _mapMatrix.Contains(gridPosition) ? false : _mapMatrix.Add(gridPosition);
    }
}

[Serializable]
internal class TemplatesSpawner
{
    [SerializeField] private MapObject _template;
    [SerializeField] private Spawner _spawner;

    public MapObject Template => _template;
    public Spawner Spawner => _spawner;

    public void Spawn(Vector3Int gridPosition,LevelGenerator generator)
    {
       _spawner.Spawn(_template,gridPosition,generator);
    }
}

