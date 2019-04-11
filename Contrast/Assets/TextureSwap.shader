Shader "Custom/TextureSwapper"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _XposCutoff ("_XposCutoff", float) = 1
        _XposStart ("_XposStart", float) = 1
        _Colour ("Primary Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Cull Off
        Blend One OneMinusSrcAlpha

        Pass
        {
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
                float4 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _Colour;
            float _XposCutoff;
            float _XposStart;
            
            v2f vert (appdata v)
            {
                v2f o;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul (unity_ObjectToWorld, v.vertex);
                o.uv = v.uv;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // IF FRIST GREATER THAN/EQUAL TO SECOND = 1
                // OTHERWISE 0
                col.a = lerp(0, col.a, step(_XposStart, i.worldPos.x));
                col.a = lerp(col.a, 0, step(_XposCutoff, i.worldPos.x));
                col.rgb *= col.a;
                
                return col;
            }
            ENDCG
        }
    }
}
