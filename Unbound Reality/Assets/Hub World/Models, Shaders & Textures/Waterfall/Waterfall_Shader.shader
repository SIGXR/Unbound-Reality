Shader "Unlit/Waterfall_Shader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		// Alpha CutOff
		_Cutoff ("Alpha Cutoff", Range(0.0, 1.0))=0.5
	}
	SubShader
	{
		Tags { "Queue"="Geometry" }

		Pass
		{
			// Enables transparency for material
			Blend SrcAlpha OneMinusSrcAlpha

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
			float _Cutoff;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//Panning
				float2 distuv = float2(i.uv.x, i.uv.y + _Time.x * 20);
				float4 color = tex2D(_MainTex, distuv);

				//Alpha Cutoff, Note: step is an if statement for shaders which returns 0 or 1
				color.a = step(0.0, i.uv.y-_Cutoff);

				return color;
			}
			ENDCG
		}
	}
}
