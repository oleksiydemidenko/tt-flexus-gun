using UnityEngine;

public class GunProjectileHitObject : MonoBehaviour 
{
    [SerializeField] private CustomRenderTexture _RenderTexture;
    [SerializeField] private Material _PaintMaterial;
    private bool _update;
    private readonly int _brushPositionPropertyIndex = Shader.PropertyToID("_BrushPosition");

    private void Awake()
    {
        GunProjectileEvents.Hit.Event += OnGunProjectileHit;
    }
    private void OnDestroy()
    {
        GunProjectileEvents.Hit.Event += OnGunProjectileHit;
    }
    private void LateUpdate() 
    {
        if (!_update) return;
        _RenderTexture.Update();
        _update = true;
    }

    private void OnGunProjectileHit(GunProjectile projectile, RaycastHit hit)
    {
        if (hit.collider.gameObject != gameObject) return;
        DrawHitTexture(hit.textureCoord);
    }

    private void DrawHitTexture(Vector2 pixelUV)
    {
        RenderTexture.active = _RenderTexture;
        _PaintMaterial.SetVector(_brushPositionPropertyIndex, new Vector4(pixelUV.x, pixelUV.y, 0, 0));
        //Graphics.Blit(null, _RenderTexture, _PaintMaterial, 0);
        RenderTexture.active = null;
        _update = true;
    }
}