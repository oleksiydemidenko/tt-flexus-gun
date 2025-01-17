using UnityEngine;

public class FeedbackTransformAnimation : FeedbackEventAnimation
{
    [Space]
    [SerializeField] public Transform _Transform;
    [SerializeField] private float _Delay;
    [SerializeField] private float _DistanceDelayPerUnit;

    [Space]
    [SerializeField] public bool _UsePosition;
    [SerializeField] public Vector3 _OffsetPosition;
    [SerializeField] private AnimationCurve _PositionCurveX;
    [SerializeField] private AnimationCurve _PositionCurveY;
    [SerializeField] private AnimationCurve _PositionCurveZ;

    [Space]
    [SerializeField] public bool _UseRotation;
    [SerializeField] public Vector3 _OffsetRotation;
    [SerializeField] private AnimationCurve _RotationCurveX;
    [SerializeField] private AnimationCurve _RotationCurveY;
    [SerializeField] private AnimationCurve _RotationCurveZ;
    
    [Space]
    [SerializeField] public bool _UseScale;
    [SerializeField] public Vector3 _OffsetScale;
    [SerializeField] private AnimationCurve _ScaleCurveX;
    [SerializeField] private AnimationCurve _ScaleCurveY;
    [SerializeField] private AnimationCurve _ScaleCurveZ;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private Vector3 _startRotation;
    private Vector3 _targetRotation;
    private Vector3 _startScale = Vector3.one;
    private Vector3 _targetScale;

    private void Awake() 
    {
        FeedbackEvents.FeedbackPosition.Event += OnFeedback;
    }
    private void OnDestroy() 
    {
        FeedbackEvents.FeedbackPosition.Event -= OnFeedback;
    }

    private void OnFeedback(string eventName, Vector3 position)
    {
        if (_FeedbackEventName != eventName) return;
        if (!IsPlaying) 
        {
            var distance = Vector3.Distance(_Transform.position, position);
            var delay = _Delay + distance * _DistanceDelayPerUnit;
            Play(delay);
        }
    }

    public override void Play(float delay)
    {
        base.Play(delay);
        _startPosition = _Transform.position;
        _startRotation = _Transform.eulerAngles;
        _startScale = _Transform.localScale;

        _targetPosition = _startPosition + _OffsetPosition;
        _targetRotation = _startRotation + _OffsetRotation;
        _targetScale = _startScale + _OffsetScale;
    }

    public virtual void Update()
    {
        UpdateTime(Time.deltaTime);
        if (!IsPlaying) return;
        if (_UsePosition) CalculatePosition();
        if (_UseRotation) CalculateRotation();
        if (_UseScale) CalculateScale();
    }

    private void CalculatePosition()
    {
        var evaluatedValueX = _PositionCurveX.Evaluate(AnimationTime);
        var evaluatedValueY = _PositionCurveY.Evaluate(AnimationTime);
        var evaluatedValueZ = _PositionCurveZ.Evaluate(AnimationTime);
        var position = new Vector3(
            Mathf.LerpUnclamped(_startPosition.x, _targetPosition.x, evaluatedValueX),
            Mathf.LerpUnclamped(_startPosition.y, _targetPosition.y, evaluatedValueY),
            Mathf.LerpUnclamped(_startPosition.z, _targetPosition.z, evaluatedValueZ));
        _Transform.position = position;
    }
    private void CalculateRotation()
    {
        var evaluatedValueX = _RotationCurveX.Evaluate(AnimationTime);
        var evaluatedValueY = _RotationCurveY.Evaluate(AnimationTime);
        var evaluatedValueZ = _RotationCurveZ.Evaluate(AnimationTime);
        var rotation = new Vector3(
            Mathf.LerpUnclamped(_startRotation.x, _targetRotation.x, evaluatedValueX),
            Mathf.LerpUnclamped(_startRotation.y, _targetRotation.y, evaluatedValueY),
            Mathf.LerpUnclamped(_startRotation.z, _targetRotation.z, evaluatedValueZ));
        _Transform.eulerAngles = rotation;
    }
    private void CalculateScale()
    {
        var evaluatedValueX = _ScaleCurveX.Evaluate(AnimationTime);
        var evaluatedValueY = _ScaleCurveY.Evaluate(AnimationTime);
        var evaluatedValueZ = _ScaleCurveZ.Evaluate(AnimationTime);
        var scale = new Vector3(
            Mathf.LerpUnclamped(_startScale.x, _targetScale.x, evaluatedValueX),
            Mathf.LerpUnclamped(_startScale.y, _targetScale.y, evaluatedValueY),
            Mathf.LerpUnclamped(_startScale.z, _targetScale.z, evaluatedValueZ));
        _Transform.localScale = scale;
    }
}