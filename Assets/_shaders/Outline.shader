Shader"Custom/OutlineShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness ("Outline Thickness", Range(0.0, 0.05)) = 0.03
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }

        Pass
        {
            ZWrite On

            ColorMask RGB

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

sampler2D _MainTex;
float4 _MainTex_ST;
float4 _OutlineColor;
float _OutlineThickness;

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    return o;
}

float4 frag(v2f i) : SV_Target
{
    float4 color = tex2D(_MainTex, i.uv);
    if (color.a == 0)
        discard;

    float alpha = tex2D(_MainTex, i.uv).a;

                // Define outline by sampling pixels around the original pixel
    float outline = 0.0;
    outline += tex2D(_MainTex, i.uv + float2(-_OutlineThickness, 0)).a;
    outline += tex2D(_MainTex, i.uv + float2(_OutlineThickness, 0)).a;
    outline += tex2D(_MainTex, i.uv + float2(0, -_OutlineThickness)).a;
    outline += tex2D(_MainTex, i.uv + float2(0, _OutlineThickness)).a;

    if (outline > 0.0)
    {
        return float4(_OutlineColor.rgb, alpha);
    }

    return color;
}
            ENDCG
        }
    }
}
