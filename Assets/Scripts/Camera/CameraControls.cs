using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private Transform _Pivot;
    [SerializeField] private float _RotateHorizontalSpeed = 100f;
    
    public void RotateHorizontal(float value)
    {
        _Pivot.rotation *= Quaternion.Euler(0, _RotateHorizontalSpeed * value * Time.deltaTime, 0);
    }
}