Shader "Rock/CurveShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BumpMap("Bumpmap", 2D) = "bump" {}
        _Curvature("Curvature", Float) = 0.001
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "RenderPipeline" = "UniversalRenderPipeline" "IgnoreProjector" = "True"}
            LOD 200

            CGPROGRAM
            #pragma surface surf Lambert vertex:vert addshadow
            uniform sampler2D _MainTex;
            uniform sampler2D _BumpMap;
            uniform float _Curvature;

            struct Input
            {
                float2 uv_MainTex;
                float2 uv_BumpMap;
            };

            void vert(inout appdata_full v)
            {
                float4 worldSpace = mul(unity_ObjectToWorld, v.vertex);
                worldSpace.xyz -= _WorldSpaceCameraPos.xyz;
                float f = (worldSpace.z * worldSpace.z) * -_Curvature * sin(_Time.y);
                worldSpace = float4(0.5f * f, 0.0f, 0.0f, 0.0f);

                v.vertex += mul(unity_WorldToObject, worldSpace);
            }

            void surf(Input IN, inout SurfaceOutput o) {
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
                o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a;
                o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            }

            ENDCG
        }
            FallBack "Mobile/Diffuse"
}