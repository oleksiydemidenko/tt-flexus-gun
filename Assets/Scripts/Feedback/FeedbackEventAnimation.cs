using UnityEngine;

public abstract class FeedbackEventAnimation : MonoBehaviour
{
    [SerializeField] protected string _FeedbackEventName;
    [field: SerializeField, Min(0)] public float Duration { get; protected set; }
    
    public bool IsPlaying { get; protected set; }
    public float AnimationTime { get; protected set; }

    protected float _speed { get; private set; }

    public virtual void Play(float delay = 0f)
    {
        _speed = 1f / (Duration == 0f ? 1f : Duration);
        AnimationTime = -delay;
        IsPlaying = true;
    }
    protected void UpdateTime(float deltaTime)
    {
        if (!IsPlaying) return;
        AnimationTime += deltaTime * _speed;
        if (AnimationTime > 1f)
        {
            AnimationTime = 1f;
            IsPlaying = false;
        }
    }
}