using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class GunControlsUI : MonoBehaviour, IPointerDownHandler, 
    IPointerUpHandler, IPointerMoveHandler
{
    [SerializeField] private Canvas _Canvas;
    [SerializeField] private Slider _PowerSlider;
    [SerializeField] private Slider _AutoModeSlider;
    [SerializeField] private float _AutoModeTimerDelay = 0.1f;
    [SerializeField] private TMP_Text _PowerValueText;
    private bool _isPointerDown;
    private float _autoModeTimer;
    private GunControls _gunControls;

    [Inject]
    private void Construct(GunControls gunControls)
    {
        _gunControls = gunControls;
    }

    private void Awake()
    {
        _PowerSlider.onValueChanged.AddListener(OnPowerSliderValueChanged);
    }
    private void OnDestroy()
    {
        _PowerSlider.onValueChanged.RemoveListener(OnPowerSliderValueChanged);
    }

    private void OnPowerSliderValueChanged(float value)
    {
        _gunControls.SetPower(value);
        _PowerValueText.text = $"{value * 100:#}";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(!_isPointerDown) return;
        _gunControls.RotateVertival(-eventData.delta.y * _Canvas.scaleFactor);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;
        _gunControls.EmitProjectile();
    }

    private void Update()
    {
        if (!_isPointerDown || _AutoModeSlider.value != 1) return;
        
        if (_autoModeTimer > 0)
        {
            _autoModeTimer -= Time.deltaTime;
            return;
        }

        _gunControls.EmitProjectile();
        _autoModeTimer = _AutoModeTimerDelay;
    }
}
