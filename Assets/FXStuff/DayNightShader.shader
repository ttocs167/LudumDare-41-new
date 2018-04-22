Shader "Custom/DayNightShader"
{
	Properties
	{
		_MainTex("Texture",2D) = "white"{}
		_Colour("Colour",Color) = (1,1,1,1)
		_Alpha("Alpha",Float) = 1				
	}
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
			};
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
			float1 _Scale;
			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float4 _Colour;
			float1 _Alpha;			
			float4 frag(v2f i) : SV_TARGET
			{
				float4 color = tex2D(_MainTex, i.uv)*_Colour*float4(1,1,1,_Alpha);
				return color;
			}
			ENDCG
		}
	}	

}
