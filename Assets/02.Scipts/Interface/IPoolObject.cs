public interface IPoolObject
{
    string Key { get; set; }
    void OnSpawnFromPool();
    void OnReturnToPool();
}
