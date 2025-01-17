using UnityEngine;

public class FeedbackCameraFOVAnimation : FeedbackEventAnimation
{
    [SerializeField] public Camera _Camera;
    [SerializeField] public float _OffsetFOV;
    [SerializeField] private AnimationCurve _FOVCurve;

    private float _startFOV;
    private float _targetFOV;

    private void Awake() 
    {
        FeedbackEvents.FeedbackPosition.Event += OnFeedback;
        _startFOV = _Camera.fieldOfView;
    }
    private void OnDestroy() 
    {
        FeedbackEvents.FeedbackPosition.Event -= OnFeedback;
    }

    private void OnFeedback(string eventName, Vector3 position)
    {
        if (_FeedbackEventName != eventName) return;
        if (!IsPlaying) Play();
    }

    public override void Play(float delay = 0f)
    {
        base.Play(delay);
        _startFOV = _Camera.fieldOfView;
        _targetFOV = _startFOV + _OffsetFOV;
    }

    public void Update()
    {
        UpdateTime(Time.deltaTime);
        if (!IsPlaying) return;
        CalculateFOV();
    }

    
    private void CalculateFOV()
    {
        var evaluatedValue = _FOVCurve.Evaluate(AnimationTime);
        var fov = Mathf.LerpUnclamped(_startFOV, _targetFOV, evaluatedValue);
        _Camera.fieldOfView = fov;
    }
}