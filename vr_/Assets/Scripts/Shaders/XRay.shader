Shader "Custom/XRay"
{
	Properties
	{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Color1("Color 1", Color) = (1,1,1,1)
		_Color2("Color 2", Color) = (1,1,1,1)
		_FresnelBias("Fresnel Bias", Float) = 0
		_FresnelScale("Fresnel Scale", Float) = 1
		_FresnelPower("Fresnel Power", Float) = 1
		_ColorPower("Color Power", Float) = 1
	}

	SubShader
	{
		Tags{"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}

		Cull Back

		Pass
		{
			ZWrite On
			ColorMask 0
		}
		Pass
		{
			Tags{ "LightMode" = "ForwardBase" }
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0

			#include "UnityCG.cginc"

			struct appdata_t
			{
			float4 pos : POSITION;
			float2 uv : TEXCOORD0;
			half3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
				float fresnel : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color1;
			fixed4 _Color2;
			fixed _FresnelBias;
			fixed _FresnelScale;
			fixed _FresnelPower;
			fixed _ColorPower;

			v2f vert(appdata_t v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.pos);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				float3 i = normalize(ObjSpaceViewDir(v.pos));
				o.fresnel = _FresnelBias + _FresnelScale * pow(1 - dot(i, v.normal), _FresnelPower);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 m = tex2D(_MainTex, i.uv);

				fixed pf = pow(i.fresnel, _ColorPower);
				fixed cl = m.r + pf;
				fixed4 c = lerp(_Color1, _Color2, cl);

				fixed f = m.r + i.fresnel;
				return  c*f ;
			}
				ENDCG
		}
	}
}