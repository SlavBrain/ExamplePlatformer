using UnityEngine;
public abstract class Spawner:MonoBehaviour
{
    public abstract void Spawn(MapObject template,Vector3Int startSpawnGridPosition,LevelGenerator generator);
}
