using UnityEngine;

public class Particles : MonoBehaviour, IPoolInstance<Particles, ParticlesType>
{
    [SerializeField] private ParticleSystem _ParticlesSystem;
    [SerializeField] private float _Lifetime = 1f;
    private float _timer;

    public ParticlesType Type { get; set; }
    public Particles MonoInstance { get; set; }
    public bool Free { get; set; }

    public void ReusePoolInstance()
    {
        Free = false;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        Free = true;
        gameObject.SetActive(false);
    }

    public void Play()
    {
        gameObject.SetActive(true);
        _timer = _Lifetime;
        _ParticlesSystem.Play();
    }

    private void Update() 
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            return;
        }
        Deactivate();
    }
}