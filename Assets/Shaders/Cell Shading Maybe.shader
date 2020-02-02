// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:7407,x:33273,y:32610,varname:node_7407,prsc:2|emission-6589-OUT;n:type:ShaderForge.SFN_Tex2d,id:2884,x:32131,y:32596,ptovrint:False,ptlb:node_2884,ptin:_node_2884,varname:node_2884,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5695b3984bbb66c489d6e69249c16a29,ntxv:0,isnm:False;n:type:ShaderForge.SFN_LightVector,id:8008,x:32115,y:32902,varname:node_8008,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:5967,x:32115,y:33036,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:6072,x:32327,y:32947,varname:node_6072,prsc:2,dt:0|A-8008-OUT,B-5967-OUT;n:type:ShaderForge.SFN_Lerp,id:6589,x:32568,y:32679,varname:node_6589,prsc:2|A-2884-RGB,B-5614-OUT,T-3601-OUT;n:type:ShaderForge.SFN_Multiply,id:5614,x:32330,y:32454,varname:node_5614,prsc:2|A-7619-OUT,B-2884-RGB;n:type:ShaderForge.SFN_Step,id:465,x:32610,y:32957,varname:node_465,prsc:2|A-6072-OUT,B-2174-OUT;n:type:ShaderForge.SFN_Vector1,id:2174,x:32447,y:33062,varname:node_2174,prsc:2,v1:0.45;n:type:ShaderForge.SFN_Fresnel,id:3010,x:32385,y:33264,varname:node_3010,prsc:2|EXP-1455-OUT;n:type:ShaderForge.SFN_Step,id:2726,x:32597,y:33333,varname:node_2726,prsc:2|A-7503-OUT,B-3010-OUT;n:type:ShaderForge.SFN_Vector1,id:7503,x:32299,y:33471,varname:node_7503,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Add,id:723,x:32819,y:33034,varname:node_723,prsc:2|A-465-OUT,B-2726-OUT;n:type:ShaderForge.SFN_Clamp01,id:3601,x:32995,y:33034,varname:node_3601,prsc:2|IN-723-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1455,x:32134,y:33315,ptovrint:False,ptlb:fresnelvalue,ptin:_fresnelvalue,varname:node_1455,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:7619,x:32091,y:32414,ptovrint:False,ptlb:shadow,ptin:_shadow,varname:node_7619,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.7;proporder:2884-1455-7619;pass:END;sub:END;*/

Shader "Custom/Cell Shading" {
    Properties {
        _node_2884 ("node_2884", 2D) = "white" {}
        _fresnelvalue ("fresnelvalue", Float ) = 2
        _shadow ("shadow", Float ) = 0.7
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_2884; uniform float4 _node_2884_ST;
            uniform float _fresnelvalue;
            uniform float _shadow;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                float4 _node_2884_var = tex2D(_node_2884,TRANSFORM_TEX(i.uv0, _node_2884));
                float3 emissive = lerp(_node_2884_var.rgb,(_shadow*_node_2884_var.rgb),saturate((step(dot(lightDirection,i.normalDir),0.45)+step(0.2,pow(1.0-max(0,dot(normalDirection, viewDirection)),_fresnelvalue)))));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_2884; uniform float4 _node_2884_ST;
            uniform float _fresnelvalue;
            uniform float _shadow;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float3 finalColor = 0;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
