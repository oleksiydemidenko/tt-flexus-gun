using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CameraControlsUI : MonoBehaviour, IPointerDownHandler, 
    IPointerUpHandler, IPointerMoveHandler
{
    [SerializeField] private Canvas _Canvas;
    
    private CameraControls _cameraControls;
    private bool _allowMove;

    [Inject]
    private void Construct(CameraControls cameraControls)
    {
        _cameraControls = cameraControls;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _allowMove = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(!_allowMove) return;
       _cameraControls.RotateHorizontal(eventData.delta.x * _Canvas.scaleFactor);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _allowMove = false;
    }
}