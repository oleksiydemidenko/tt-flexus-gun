Shader "Custom/Painter"
{
    Properties
    {
        _BrushColor ("Brush Color", Color) = (1, 1, 1, 1)
        _BrushTex ("Brush Texture", 2D) = "white" {}
        _BrushSize ("Brush Position", Range(0, 1)) = 1
        _BrushPosition ("Brush Position", Vector) = (0.5, 0.5, 0, 0)
        _BrushPositionX ("Brush Position X", Float) = 0
    }
    SubShader
    {
        Lighting Off
        Blend One Zero
        ZTest Always 
        Cull Off 
        ZWrite Off 
        Fog { Mode Off }
        Tags { "RenderTexture"="CustomRenderTexture" }
        
        Pass
        {
            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 2.0
            
            sampler2D _BrushTex;
            float4 _BrushTex_ST;
            half4 _BrushPosition;
            float4 _BrushColor;
            float _BrushSize;
            
            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float4 previousColor = lerp(tex2D(_SelfTexture2D, IN.localTexcoord.xy), 
                    float4(1, 1, 1, 1), _BrushPosition.z--);

                half2 brushPosition = IN.localTexcoord.xy - _BrushPosition;
                brushPosition /= _BrushSize;
                brushPosition += float2(0.5, 0.5);
                float4 brushColor = tex2D(_BrushTex, brushPosition) * _BrushColor;
                return lerp(previousColor, brushColor, brushColor.a);
            }
            ENDCG
        }
    }
}
