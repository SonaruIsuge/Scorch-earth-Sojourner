
public interface IControllable
{
    bool isEnableControl { get; }
    void EnableControl(bool enable, IInput input = null);
}
