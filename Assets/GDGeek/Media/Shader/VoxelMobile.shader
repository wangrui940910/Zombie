// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Voxel/Mobile" {
Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 150

CGPROGRAM
#pragma surface surf Lambert noforwardadd



fixed4 _Color;
struct Input {
	float2 uv_MainTex;
	fixed3 color : COLOR;
	float4 vertex : POSITION; 
};

void surf (Input IN, inout SurfaceOutput o) {
	//o.Albedo = IN.color;


	//o.Alpha = 1;//c.a;
	o.Albedo = IN.color* _Color.rgb;

	o.Alpha = 0;
	//o.Alpha = _Color.a;//1;//IN.color.a;
	//o.	 = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
}
ENDCG
}

Fallback "Mobile/VertexLit"
}
