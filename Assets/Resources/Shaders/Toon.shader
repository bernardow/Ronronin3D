Shader "Unlit/Toon"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR][Space][Header(Shadow)]
        _ShadowColor("ShadowColor", Color) = (0,0,0,0)
        [Space][Header(Rim)]
        _RimAmount("RimAmount", Range(0, 1)) = 0.7
        _RimThreshold("RimThreshold", Range(0, 1)) = 0.5
        [Space][Header(Fresnel)]
        _FresnelPower("FresnelPower", Float) = 1
        _FresnelBorder("FresnelBorder", Float) = 1
        _FresnelColor("FresnelColor", Color) = (1,1,1,1)
        [Space][Header(Gloss)]
        _Gloss("Gloss", Range(0, 1)) = 0.5
        
    }
    SubShader
    {
        Tags 
        {
             "LightMode"="UniversalForward" 
        }
        
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float3 normals : TEXCOORD1;
                float4 vertex : SV_POSITION;
                float3 wPos : TEXTCOORD2;
                SHADOW_COORDS(2)
            };

            float4 _ShadowColor;
            float _RimAmount;
            float _RimThreshold;

            float _Gloss;
            
            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _FresnelPower;
            float _FresnelBorder;
            float4 _FresnelColor;
            
            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normals = UnityObjectToWorldNormal(v.normal);
                o.wPos = mul(unity_ObjectToWorld, v.vertex);
                TRANSFER_SHADOW(o)
                return o;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                //Light
                float3 L = _WorldSpaceLightPos0;
                float3 V = normalize(_WorldSpaceCameraPos - i.wPos);
                float3 N = normalize(i.normals);
                float3 H = normalize(L + V);
                float3 lambert = dot(L, N);
                float shadow = SHADOW_ATTENUATION(i);
                float3 lightIntensity = smoothstep(0, 0.1, lambert * shadow);
                float3 diffuseLight = lightIntensity * _LightColor0;

                //Specular Light
                float3 specularLight = saturate(dot(H, N)) * (lambert > 0);
                _Gloss = exp2(_Gloss * 10);
                specularLight = smoothstep(0.005, 0.1, (pow(specularLight, _Gloss)));

                //Rim
                float4 rimDot = 1 - dot(V, N);
                float rimIntensity = rimDot * pow(lambert, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.1, _RimAmount + 0.1, rimIntensity);
                float3 rim = rimDot * rimIntensity;

                //Fresnel
                float4 fresnel = smoothstep(_FresnelBorder - 0.1, _FresnelBorder + 0.1, pow(rimDot, _FresnelPower)) * _FresnelColor;
                
                //Out
                float4 col = tex2D(_MainTex, i.uv);
                float4 outPut = float4(col * (diffuseLight + _ShadowColor + specularLight + rim) + fresnel, 1);
                return outPut;
            }
            ENDCG
        }
    }
}
