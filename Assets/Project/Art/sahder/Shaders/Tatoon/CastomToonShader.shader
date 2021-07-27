Shader "CelShading/ToonShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("Normal/Bump Map", 2D) = "bump" {}
		_Color ("Tint Color", Color) = (1,1,1,1)
		_LightCutoff("Light cutoff", Range(0,1)) = 0.5
        _ShadowBands("Shadow bands", Range(1,4)) = 1
		_SpecularColor("Specular Color", Color) = (1, 1, 1, 1)
		_Smoothness("Smoothness", float) = 1

		_Glossiness ("Smoothness", Range(0,1)) = 0.5

		[Header(Rim)]	
        _RimSize("Rim size", Range(-0.01, 1)) = 0
		[HDR]
		_RimColor("Rim color", Color) = (0,0,0,1)
		[HDR]
		_Emission("Emission", Color) = (0,0,0,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

		Stencil
		{
			Ref 1
			Comp always
			Pass replace
			ZFail keep
		}

        CGPROGRAM
		#pragma surface surf Cel
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		fixed4 _Color;
		float _LightCutoff;
        float _ShadowBands;
		float _Smoothness;
		fixed4 _SpecularColor;

		float _RimSize;
        fixed4 _RimColor;

		half _Glossiness;

		fixed4 _Emission;

		float4 LightingCel(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
		{
			half nDotL = saturate(dot(s.Normal, normalize(lightDir)));
			half diff = round(saturate(nDotL / _LightCutoff) * _ShadowBands) / _ShadowBands;
			half stepAtten = round(atten);
            half shadow = diff * stepAtten;
			float3 refl = reflect(normalize(lightDir), s.Normal);
            float vDotRefl = dot(viewDir, -refl);
			float3 rim = _RimColor * step(1 - _RimSize ,1 - saturate(dot(normalize(viewDir), s.Normal)));

			float3 specular = _SpecularColor.rgb * step(1 - _Smoothness, vDotRefl);
			half3 col = (s.Albedo + specular) * _LightColor0;

			half4 c;
			#ifdef SHADOWED_RIM
            c.rgb = (col + rim) * shadow;
            #else
            c.rgb = col * shadow + rim;
            #endif            
			c.a = s.Alpha;
			return c;
		}

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_BumpMap;
        };

		//https://answers.unity.com/questions/356198/dynamic-batching-vertex-limit.html
		//optimization: More than 900 total vert attributes

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
			o.Emission = o.Albedo * _Emission;
			o.Alpha = c.a;
        }

        ENDCG
    }

    FallBack "Diffuse"
}