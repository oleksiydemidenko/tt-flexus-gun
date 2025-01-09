using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Material _PainterMaterial;
    [SerializeField] private CustomRenderTexture _RenderTexture;

    private void Awake() 
    {
        Application.targetFrameRate = 60;
        ClearRenderTexture();
    }

    private void ClearRenderTexture()
    {
        _PainterMaterial.SetVector("_BrushPosition", new Vector4(-1, -1, 1, 0));
        RenderTexture.active = _RenderTexture;
        Graphics.Blit(null, _RenderTexture, _PainterMaterial, 0);  
        RenderTexture.active = null;
       _RenderTexture.Update();
    }
}