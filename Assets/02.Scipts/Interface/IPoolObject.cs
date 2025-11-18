public interface IPoolObject
{
    string Key { get; set; }
    /// <summary>
    /// pool에서 Get될 때 실행되는 메서드
    /// </summary>
    void OnSpawnFromPool();
    /// <summary>
    /// pool에서 Release될 때 실행되는 메서드
    /// </summary>
    void OnReturnToPool();
}
