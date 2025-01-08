using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControlsUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    [SerializeField] private Canvas _Canvas;
    [SerializeField] private CameraControls _CameraControls;
    private bool _allowMove;

    public void OnPointerDown(PointerEventData eventData)
    {
        _allowMove = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(!_allowMove) return;
        _CameraControls.RotateHorizontal(eventData.delta.x * _Canvas.scaleFactor);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _allowMove = false;
    }
}