using System.Collections;
using UnityEngine;

public class FeedbackParticleEvent : MonoBehaviour
{
    [SerializeField] private string _FeedbackEventName;
    [SerializeField] private float _Delay;
    [SerializeField] private ParticleSystem _Particle;

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
        StartCoroutine(PlayDelayed());
    }
    private IEnumerator PlayDelayed()
    {
        yield return new WaitForSeconds(_Delay);
        _Particle.Play();
    }
}