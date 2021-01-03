Shader "Disolve"
{
    Properties
    {
        [KeywordEnum(X, Y, Z)] _DisolveDirection("Disolve Direction", float) = 0
        [Toggle] _Invert ("Invert?", Float) = 0
        _MainTex ("Texture", 2D) = "white" {}
        _Noise ("Noise", 2D) = "white" {}
        _DisolveHeight("Disolve Value", Range(-1,1)) = 0.5
        _DisolveRangeMultiplyer("Dosolve Range", Range(0,100)) = 0.1
        _DisolveDetailLevel ("Detail Level", Range(1,10)) = 2
        _Color ("Disolve Border Color", Color) = (1,1,1,1)
        _AddColor ("Additional Color", Color) = (1,1,1,1)        
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Opaque" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            #pragma multi_compile _DISOLVEDIRECTION_X _DISOLVEDIRECTION_Y _DISOLVEDIRECTION_Z
            #pragma multi_compile __ _INVERT_ON

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD2;
                float4 vertex : SV_POSITION;
                float4 localPos : TEXCOORD1;
            };

            sampler2D _MainTex; float4 _MainTex_ST;
            sampler2D _Noise; float4 _Noise_ST;
            float _DisolveHeight;
            float _DisolveRangeMultiplyer;
            float4 _Color;
            float4 _AddColor;
            float _DisolveDirection;
            int _DisolveDetailLevel;

            v2f vert (appdata v)
            {
                v2f o;
                
                o.localPos = v.vertex;                
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);                
                o.uv2 = TRANSFORM_TEX(v.uv, _Noise);                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 noise = tex2D(_Noise, i.uv2);
                fixed4 detailnoise = tex2D(_Noise, i.uv2*3);
                
                float dir = i.localPos.x;                
                #if defined (_DISOLVEDIRECTION_Y)
                    dir = i.localPos.y;
                #elif defined (_DISOLVEDIRECTION_Z)
                    dir = i.localPos.z;
                #endif
                
                #if defined (_INVERT_ON)
                    float mask = lerp(0, 1, (dir - _DisolveHeight*_DisolveRangeMultiplyer));
                #else
                    float mask = lerp(1, 0, (dir - _DisolveHeight*_DisolveRangeMultiplyer));
                #endif
                
                mask = saturate(mask+noise);                
                mask = clamp((ceil(lerp(mask*noise,1,mask)*_DisolveDetailLevel))/_DisolveDetailLevel,0,1);
                float4 disolveColor = lerp (_Color,_AddColor,noise);
                col = lerp(disolveColor,col, saturate(mask-0.5)*2);
                col.a = ceil(mask);
                
                return col;
            }
            ENDCG
        }
    }
}