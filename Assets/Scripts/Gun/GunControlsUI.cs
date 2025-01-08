using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GunControlsUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    [SerializeField] private Canvas _Canvas;
    [SerializeField] private GunControls _GunControls;
    [SerializeField] private Slider _PowerSlider;
    [SerializeField] private TMP_Text _PowerValueText;
    private bool _allowMove;

    private void Awake() {
        _PowerSlider.onValueChanged.AddListener(OnPowerSliderValueChanged);
    }
    private void OnDestroy() {
        _PowerSlider.onValueChanged.RemoveListener(OnPowerSliderValueChanged);
    }

    private void OnPowerSliderValueChanged(float value)
    {
        _GunControls.SetPower(value);
        _PowerValueText.text = $"{value * 100:#}";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _allowMove = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(!_allowMove) return;
        _GunControls.RotateVertival(-eventData.delta.y * _Canvas.scaleFactor);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _allowMove = false;
        _GunControls.EmitProjectile();
    }
}
