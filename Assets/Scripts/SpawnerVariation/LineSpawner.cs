using UnityEngine;
public class LineSpawner : Spawner
{
    [SerializeField] private int _minCount=2;
    [SerializeField] private int _maxCount = 10;

    public override void Spawn(MapObject template, Vector3Int startSpawnGridPosition, LevelGenerator generator)
    {
        int count = Random.Range(_minCount, _maxCount);

        Instantiate(template, generator.GridToWorldPosition(startSpawnGridPosition), generator.transform.rotation, generator.transform);

        for (int i = 1; i < count; i++) 
        {
            Vector3Int currentGridPosition = startSpawnGridPosition + i * new Vector3Int(1,0,0);

            if (generator.TryAddNewPositionInGrid(currentGridPosition))
            {
                Instantiate(template, generator.GridToWorldPosition(currentGridPosition), generator.transform.rotation, generator.transform);
            }
            else
            {
                break;
            }
        }
    }
}
