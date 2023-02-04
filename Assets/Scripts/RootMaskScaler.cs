using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMaskScaler : MonoBehaviour
{
    [Range(0.01f,1)]
    [SerializeField] private float _rootMask = 0.5f;
    [SerializeField] private RectTransform _mask;
    [SerializeField] private RectTransform _roots;

    void Update()
    {
        float currentMaskScale = _mask.localScale.x;
        float currentRootScale = _roots.localScale.x;
        
        Vector3 newMaskScale = new Vector3(_rootMask, _rootMask, _rootMask);

        float newRootScaler = currentRootScale + (-1f * (currentMaskScale - _rootMask));
        Vector3 newRootScale = new Vector3(1/_rootMask, 1/_rootMask, 1/_rootMask);

        _mask.localScale = newMaskScale;
        _roots.localScale = newRootScale;
    }
}
