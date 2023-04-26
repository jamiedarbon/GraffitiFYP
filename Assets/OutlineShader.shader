Shader "Sleepy/OutlinePP" {

    Properties{
    _MainTex("Texture", 2D) = "white" {}
    _NormalMult("Normal Outline Multiplier", Range(0,4)) = 1
    _NormalBias("Normal Outline Bias", Range(1,4)) = 1
    _DepthMult("Depth Outline Multiplier", Range(0,4)) = 1
    _DepthBias("Depth Outline Bias", Range(1,4)) = 1
    }

        SubShader{
        Tags {"Queue" = "Overlay" "RenderType" = "Opaque"}
        Cull Off
        ZWrite Off
        ZTest Always

        Pass{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _CameraDepthNormalsTexture;
            float4 _CameraDepthNormalsTexture_TexelSize;

            float _NormalMult;
            float _NormalBias;
            float _DepthMult;
            float _DepthBias;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v) {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            void Compare(inout float depthOutline, inout float normalOutline,
                float baseDepth, float3 baseNormal, float2 uv, float2 offset) {
                float4 neighborDepthnormal = tex2D(_CameraDepthNormalsTexture,
                    uv + _CameraDepthNormalsTexture_TexelSize.xy * offset);
                float3 neighborNormal;
                float neighborDepth;
                DecodeDepthNormal(neighborDepthnormal, neighborDepth, neighborNormal);
                neighborDepth = neighborDepth * _ProjectionParams.z;

                float3 normalDifference = baseNormal - neighborNormal;
                normalOutline += normalDifference;
            }

            fixed4 frag(v2f i) : SV_Target{
                float4 depthnormal = tex2D(_CameraDepthNormalsTexture, i.uv);

                float3 normal;
                float depth;
                DecodeDepthNormal(depthnormal, depth, normal);

                depth = depth * _ProjectionParams.z;

                float depthDifference = 0;
                float normalDifference = 0;

                Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(1, 0));
                Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(0, 1));
                Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(0, -1));
                Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(-1, 0));

                depthDifference = depthDifference * _DepthMult;

                normalDifference = normalDifference * _NormalMult;
                normalDifference = saturate(normalDifference);
                normalDifference = pow(normalDifference, _NormalBias);

                float outline = normalDifference + depthDifference;
                float4 sourceColor = tex2D(_MainTex, i.uv);
                float4 color = lerp(sourceColor, 0, outline);
                return color;
            }
            ENDCG
        }
    }
}