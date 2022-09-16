
using UnityEngine;

public interface IInput
{
    float vertical { get; }
    float horizontal { get; }
    bool interact { get; }
    bool enablePhoto { get; }
    Vector2 controlFrameArea { get; }
    bool takePhoto { get; }

    void Register();
    void Unregister();
    void ReadInput();
}
