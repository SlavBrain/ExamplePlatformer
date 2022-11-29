using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectLine : MapObject
{ 
    [SerializeField] private MapObject _template;
    private int _maxCount = 10;
    private int _minCount = 3;

    public void Init(Vector3Int startGridPosition,Quaternion rotation,Transform parent, LevelGenerator generator)
    {
        int count = Random.Range(_minCount, _maxCount);

        for(int i = 1; i <= count; i++)
        {
            Vector3Int currentGridPosition = startGridPosition + i * new Vector3Int(1, 0, 0);

            if (generator.TryAddNewCellInMapMatrix(currentGridPosition))
            {
                Instantiate(_template,generator.GridToWorldPosition( currentGridPosition), rotation, parent);
            }   
            else
            {
                break;
            }
        }
    }
}
