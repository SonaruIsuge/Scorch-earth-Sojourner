using UnityEngine;
using System.Collections;

public class MachineShake : MonoBehaviour
{

    private Vector3 originalPos;
	
    void Awake()
    {
        originalPos = transform.localPosition;
    }
    

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            var x = Random.Range(-1f, 1f) * magnitude;
            var y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.localPosition = originalPos;
    }
}
