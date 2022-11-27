

using System.Collections.Generic;
using UnityEngine;public class OutlineMaterialSwitcher : MonoBehaviour, IMaterialSwitcher
{
    [field: SerializeField] public Material defaultMaterial { get; private set; }
    [field: SerializeField] public Material replaceMaterial { get; private set; }
    [field: SerializeField] public List<Renderer> changeRenderer { get; private set; }
    
    public void ReplaceMaterial()
    {
        foreach (var r in changeRenderer) r.material = replaceMaterial;
    }

    public void ResetMaterial()
    {
        foreach (var r in changeRenderer) r.material = defaultMaterial;
    }
}
