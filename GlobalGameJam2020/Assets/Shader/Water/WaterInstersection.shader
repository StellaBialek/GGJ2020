Shader "Custom/WaterIntersection"
{
	Properties
	{
	   _Color("Main Color", Color) = (1, 1, 1, .5)
	   _IntersectionTexture("IntersectionTexture", 2D) = "white"{}
	   _IntersectionColor("Intersection Color", Color) = (1, 1, 1, .5)
	   _IntersectionThresholdMax("Intersection Threshold Max", float) = 1
	   _DisplGuide("Displacement guide", 2D) = "white" {}
	   _DisplAmount("Displacement amount", float) = 0
	   _NoiseColor("Noise1 Color", Color) = (1, 1, 1, .5)
	   _NoiseTexture("Noise1 Texture", 2D) = "white" {}
	   _NoiseSpeed("Floating Speed1", float) = 0
	   _NoiseColor2("Noise1 Color", Color) = (1, 1, 1, .5)
	   _NoiseTexture2("Noise1 Texture", 2D) = "white" {}
	   _NoiseSpeed2("Floating Speed1", float) = 0
	}
		SubShader
	   {
		   Tags { "Queue" = "Transparent" "RenderType" = "Transparent"  }

		   Pass
		   {
			  Blend SrcAlpha OneMinusSrcAlpha
			  ZWrite Off

			  CGPROGRAM
			  #pragma vertex vert
			  #pragma fragment frag
			  #pragma multi_compile_fog
			  #include "UnityCG.cginc"

			  struct appdata
			  {
				  float4 vertex : POSITION;
				  float2 uv : TEXCOORD0;
			  };

			  struct v2f
			  {
				  float2 uv : TEXCOORD0;
				  UNITY_FOG_COORDS(1)
				  float4 vertex : SV_POSITION;
				  float2 intersectionUV : TEXCOORD2;
				  float2 displUV : TEXCOORD3;
				  float4 scrPos : TEXCOORD4;
				  float2 noiseUV : TEXCOORD5;
				  float2 noiseUV2 : TEXCOORD6;
			  };

			  sampler2D _CameraDepthTexture;
			  float4 _Color;
			  float4 _IntersectionColor;
			  sampler2D _IntersectionTexture;
			  float4 _IntersectionTexture_ST;
			  float _IntersectionThresholdMax;
			  sampler2D _DisplGuide;
			  float4 _DisplGuide_ST;
			  float4 _NoiseColor;
			  sampler2D _NoiseTexture;
			  float4 _NoiseTexture_ST;
			  float _NoiseSpeed;
			  float4 _NoiseColor2;
			  sampler2D _NoiseTexture2;
			  float4 _NoiseTexture2_ST;
			  float _NoiseSpeed2;

			  v2f vert(appdata v)
			  {
				  v2f o;
				  o.vertex = UnityObjectToClipPos(v.vertex);
				  o.scrPos = ComputeScreenPos(o.vertex);
				  o.intersectionUV = TRANSFORM_TEX(v.uv, _IntersectionTexture);
				  o.displUV = TRANSFORM_TEX(v.uv, _DisplGuide);
				  o.noiseUV = TRANSFORM_TEX(v.uv, _NoiseTexture) + _Time/_NoiseSpeed;
				  o.noiseUV2 = TRANSFORM_TEX(v.uv, _NoiseTexture2) + _Time/_NoiseSpeed2;
				  o.uv = v.uv;
				  UNITY_TRANSFER_FOG(o,o.vertex);
				  return o;
			  }

			  half _DisplAmount;

			   half4 frag(v2f i) : SV_TARGET
			   {
				  float depth = LinearEyeDepth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)));

				  float2 displ = tex2D(_DisplGuide, i.displUV - _Time.y / 5).xy;
				  displ = ((displ * 2) - 1) * _DisplAmount;

				  float diff = (saturate(_IntersectionThresholdMax * (depth - i.scrPos.w) + displ * tex2D(_NoiseTexture, i.noiseUV)));

				  //col = lerp(fixed4(1, 1, 1, 1), col, step(_BottomFoamThreshold, i.uv.y + displ.y));

				  //fixed4 col = lerp(tex2D(_IntersectionTexture, i.intersectionUV), _Color, step(0.5, displ.y + diff));

				  //_Color = lerp(_Color, tex2D(_NoiseTexture, i.noiseUV), 0.3);

				  float4 _LightenColor = tex2D(_NoiseTexture, i.noiseUV) * _NoiseColor;
				  float alphaValue = _Color.a;
				  //_Color = float4(max(_Color.r, _LightenColor.r),max(_Color.g, _LightenColor.g),max(_Color.b, _LightenColor.b),_Color.a);
				  _Color = 1-(1-_Color)*(1-_LightenColor);
				  float4 _SecondColor = tex2D(_NoiseTexture2, i.noiseUV2) * _NoiseColor2;
				  _Color = 1-(1-_Color)*(1-_SecondColor);
				  _Color.a = alphaValue;
				  fixed4 col = lerp(_IntersectionColor, _Color, step(0.5, diff));

				  UNITY_APPLY_FOG(i.fogCoord, col);
				  return col;
			   }

			   ENDCG
		   }
	   }
		   FallBack "VertexLit"
}