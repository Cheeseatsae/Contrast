Shader "Unlit/TextureSwap"
{

    // shader https://pastebin.com/FiDzFixp
    // script https://pastebin.com/gq46mBvf

    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SecondaryTex ("Texture", 2D) = "white" {}
        
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        
        LOD 200
        
        ZWrite Off // turns off depth buffer so object is actually transparent
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct Input {
                float2 uv_MainTex : TEXCOORD0;
                float3 worldPos;
                float3 worldNormal;
            }

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
            float4 _MainTex_ST;
            sampler2D _SecondaryTex;
            
            float _SwapPoint;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {   
                half4 c = tex2D(_MainTex, IN.uv_MainTex);
                half4 c2 = tex2D(_SecondaryTex, IN.uv_MainTex);
                
                             
                float dis = distance(_SwapPoint, IN.worldPos)
                
                fixed4 col = tex2D(_texture, i.uv);
                
                return col;
            }
            ENDCG
        }
    }
}
