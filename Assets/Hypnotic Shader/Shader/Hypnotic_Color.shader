Shader "Langvv/Hypnotic_Color"
{
	Properties 
	{
		_MainTexture("Main texture", 2D) = "black" {}
		_MainColor("Main Color", Color) = (1,1,1,1)
		_Range1("Range 1", Range(0.1,5) ) = 0.5
		_Range2("Range 2", Range(1,100) ) = 50
		_Speed("Speed", Float) = 1
		_HiPower("Hipnotic power", Float) = 1

	}
	
	SubShader 
	{
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="False"
			"RenderType"="Opaque"

		}

		
		Cull Back
		ZWrite On
		ZTest LEqual
		ColorMask RGBA
		Fog{
		}


		CGPROGRAM
		#pragma surface surf BlinnPhongEditor  alpha decal:add vertex:vert
		#pragma target 2.0


		sampler2D _MainTexture;
		float4 _MainColor;
		float _Range1;
		float _Range2;
		float _Speed;
		float _HiPower;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
				half3 spec = light.a * s.Gloss;
				half4 c;
				c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
				c.a = s.Alpha;
				return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}
			
			struct Input {
				float2 uv_MainTexture;
				float4 meshUV;

			};

			void vert (inout appdata_full v, out Input o) {
				float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
				float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
				float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
				float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);

				o.meshUV.xy = v.texcoord.xy;
				o.meshUV.zw = v.texcoord1.xy;

			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
				float4 Tex2D1=tex2D(_MainTexture,(IN.uv_MainTexture.xyxy).xy);
				float4 Multiply5=Tex2D1 * _MainColor;
				float4 Subtract0=float4( 1,1,0,0) - (IN.meshUV.xyxy);
				float4 Mask3=float4(Subtract0.x,Subtract0.y,0.0,0.0);
				float4 Subtract2=Mask3 - float4( 0.5,0.5,0,0);
				float4 Multiply0=Subtract2 * float4( 2,2,2,2 );
				float4 Dot0=dot( Multiply0.xyz, Multiply0.xyz ).xxxx;
				float4 Subtract1=float4( 1.0, 1.0, 1.0, 1.0 ) - Dot0;
				float4 Pow0=pow(Subtract1,_Range1.xxxx);
				float4 Multiply1=Pow0 * _Range2.xxxx;
				float4 Multiply4=_Time * _Speed.xxxx;
				float4 Add0=Multiply1 + Multiply4;
				float4 Sin0=sin(Add0);
				float4 Multiply2=Multiply5 * Sin0;
				float4 Multiply3=Multiply2 * _HiPower.xxxx;
				float4 Master0_0_NoInput = float4(0,0,0,0);
				float4 Master0_1_NoInput = float4(0,0,1,1);
				float4 Master0_3_NoInput = float4(0,0,0,0);
				float4 Master0_4_NoInput = float4(0,0,0,0);
				float4 Master0_5_NoInput = float4(1,1,1,1);
				float4 Master0_7_NoInput = float4(0,0,0,0);
				float4 Master0_6_NoInput = float4(1,1,1,1);
				o.Emission = Multiply3;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}