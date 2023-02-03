using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class FieldOfViewChanger : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _fixedHorizontalFOV = 34f;

    private float _initialFOV;
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _initialFOV = _virtualCamera.m_Lens.FieldOfView;
    }

    private void Start()
    {
        float fieldOfView = 2 * Mathf.Atan(Mathf.Tan(_fixedHorizontalFOV * Mathf.Deg2Rad * 0.5f) / _mainCamera.aspect)
                              * Mathf.Rad2Deg;

        _virtualCamera.m_Lens.FieldOfView = fieldOfView < _initialFOV ? _initialFOV : fieldOfView;
    }
}