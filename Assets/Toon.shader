﻿Shader "Sleepy/Toon"
{
	Properties
	{
		_Colour("Colour", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}
		[HDR]
		_AmbientColour("Ambient Colour", Color) = (0.4,0.4,0.4,1)
	}
		SubShader
	{
		Tags {
		"RenderType" = "Opaque"
		"Queue" = "Geometry"
		}
		Pass
		{
			Tags {
			"LightMode" = "ForwardBase"
			"PassFlags" = "OnlyDirectional"
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : NORMAL;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				return o;
			}

			float4 _Colour;
			float4 _AmbientColour;

			float4 frag(v2f i) : SV_Target
			{
				float3 normal = normalize(i.worldNormal);
				float NdotL = dot(_WorldSpaceLightPos0, normal);
				float lightIntensity = NdotL > 0 ? 1 : 0;
				float4 light = lightIntensity * _LightColor0;
				float4 sample = tex2D(_MainTex, i.uv);

				return _Colour * sample * (_AmbientColour + light);
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}