// Shader created with Shader Forge Beta 0.28 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.28;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,hqsc:True,hqlp:False,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.4338235,fgcg:0.4338235,fgcb:0.4338235,fgca:1,fgde:0.015,fgrn:0,fgrf:3466.6,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|emission-26-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33439,y:32755,ptlb:MainTex,ptin:_MainTex,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:9,x:33423,y:32536;n:type:ShaderForge.SFN_Multiply,id:10,x:33192,y:32665|A-9-RGB,B-2-RGB;n:type:ShaderForge.SFN_Color,id:12,x:33348,y:32388,ptlb:Color,ptin:_Color,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:13,x:33025,y:32599|A-12-RGB,B-10-OUT;n:type:ShaderForge.SFN_Slider,id:22,x:33391,y:33012,ptlb:node_22,ptin:_node_22,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:26,x:32991,y:32818|A-13-OUT,B-29-OUT,T-22-OUT;n:type:ShaderForge.SFN_Vector1,id:29,x:33233,y:32879,v1:0;proporder:2-12-22;pass:END;sub:END;*/

Shader "Shader Forge/Diffuse_VertexColor" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _node_22 ("node_22", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _Color;
            uniform float _node_22;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 node_34 = i.uv0;
                float node_29 = 0.0;
                float3 emissive = lerp((_Color.rgb*(i.vertexColor.rgb*tex2D(_MainTex,TRANSFORM_TEX(node_34.rg, _MainTex)).rgb)),float3(node_29,node_29,node_29),_node_22);
                float3 finalColor = emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
