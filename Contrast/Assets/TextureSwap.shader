Shader "Custom/TextureSwapper"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _LineWidth ("Line Width", range(0,1)) = 0.05
        _LineColour ("Line Colour", color) = (1,1,1,1)
        _XposCutoff ("_XposCutoff", float) = 1
        _XposStart ("_XposStart", float) = 1
        
        _Distance("Distance", Range(0,2)) = 1.16
		_Amplitude("Amplitude", float) = 420
		_Speed("Speed", Range(0,2)) = 0.51
		_Amount("Amount", Range(0,2)) = 0.4
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
            fixed4 _LineColour;
            float _XposCutoff, _XposStart, _LineWidth;
            float _Distance, _Amplitude, _Speed, _Amount;
            
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
                
                // worldpos.x - noise amount = new tex
                
                // IF FRIST GREATER THAN/EQUAL TO SECOND = 1
                // OTHERWISE 0
                col.a = lerp(0, col.a, step(_XposStart, i.worldPos.x));
                col.a = lerp(col.a, 0, step(_XposCutoff, i.worldPos.x));
                
            //    sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * _Amount;
              
                float width = sin((_SinTime.y * _Speed + _LineWidth * _Amplitude) * i.worldPos.y) * _Distance * _Amount;
                float width2 = sin((_SinTime.y * _Speed + _LineWidth * _Amplitude) * i.worldPos.y) * _Distance * _Amount * -1;
                
                // changing colour of texture to line colour
                col.rgb = lerp(_LineColour * _SinTime, col.rgb, step(_XposStart + (width * _SinTime), i.worldPos.x));
                col.rgb = lerp(col.rgb, _LineColour * _SinTime, step(_XposCutoff - (width2 * _SinTime), i.worldPos.x));
                
                col.rgb *= col.a;
                
                return col;
            }
            ENDCG
        }
    }
}
