
using UnityEngine;

public interface IInput
{
    float vertical { get; }
    float horizontal { get; }
    bool interact { get; }


    void Create();
    void Destroy();
    void ReadInput();
}
