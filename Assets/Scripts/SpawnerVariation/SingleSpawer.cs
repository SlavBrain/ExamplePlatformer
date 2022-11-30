using UnityEngine;

public class SingleSpawer : Spawner
{

    public override void Spawn(MapObject template, Vector3Int startSpawnGridPosition, LevelGenerator generator)
    {
        Instantiate(template,generator.GridToWorldPosition(startSpawnGridPosition),generator.transform.rotation,generator.transform);
    }
}
