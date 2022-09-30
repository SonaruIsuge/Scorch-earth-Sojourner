
public interface ICameraFeature
{
    MemoryCamera owner { get; }
    bool enable { get; }

    void EnableFeature(bool enable);
}
