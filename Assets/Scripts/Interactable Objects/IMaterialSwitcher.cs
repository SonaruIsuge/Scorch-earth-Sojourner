

using System.Collections.Generic;
using UnityEngine;

public interface IMaterialSwitcher
{
    Material defaultMaterial { get; }
    Material replaceMaterial { get; }
    List<Renderer> changeRenderer { get; }

    void ReplaceMaterial();
    void ResetMaterial();

}
