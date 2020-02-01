// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2575,x:32719,y:32712,varname:node_2575,prsc:2|emission-7132-OUT;n:type:ShaderForge.SFN_SceneColor,id:1981,x:32233,y:32920,varname:node_1981,prsc:2|UVIN-9061-UVOUT;n:type:ShaderForge.SFN_Fresnel,id:8465,x:32121,y:32726,varname:node_8465,prsc:2|EXP-7835-OUT;n:type:ShaderForge.SFN_Add,id:7132,x:32498,y:32803,varname:node_7132,prsc:2|A-1452-OUT,B-1981-RGB;n:type:ShaderForge.SFN_ScreenPos,id:9061,x:32059,y:32920,varname:node_9061,prsc:2,sctp:2;n:type:ShaderForge.SFN_ValueProperty,id:7853,x:31427,y:32775,ptovrint:False,ptlb:pulse_strength,ptin:_pulse_strength,varname:node_7853,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.3;n:type:ShaderForge.SFN_Time,id:5016,x:31321,y:32926,varname:node_5016,prsc:2;n:type:ShaderForge.SFN_Sin,id:4251,x:31506,y:32926,varname:node_4251,prsc:2|IN-5016-TDB;n:type:ShaderForge.SFN_Multiply,id:2528,x:31658,y:32863,varname:node_2528,prsc:2|A-7853-OUT,B-4251-OUT;n:type:ShaderForge.SFN_RemapRange,id:7835,x:31856,y:32863,varname:node_7835,prsc:2,frmn:0,frmx:1,tomn:1,tomx:2|IN-2528-OUT;n:type:ShaderForge.SFN_Lerp,id:1452,x:32342,y:32679,varname:node_1452,prsc:2|A-5320-OUT,B-9304-RGB,T-8465-OUT;n:type:ShaderForge.SFN_Color,id:9304,x:31944,y:32591,ptovrint:False,ptlb:Bubble_Color,ptin:_Bubble_Color,varname:node_9304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5078936,c2:0.5588877,c3:0.8970588,c4:1;n:type:ShaderForge.SFN_Vector1,id:5320,x:32342,y:32600,varname:node_5320,prsc:2,v1:0;proporder:7853-9304;pass:END;sub:END;*/

Shader "Custom/TranslucentPulse" {
    Properties {
        _pulse_strength ("pulse_strength", Float ) = 0.3
        _Bubble_Color ("Bubble_Color", Color) = (0.5078936,0.5588877,0.8970588,1)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float _pulse_strength;
            uniform float4 _Bubble_Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 projPos : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float node_5320 = 0.0;
                float4 node_5016 = _Time;
                float3 emissive = (lerp(float3(node_5320,node_5320,node_5320),_Bubble_Color.rgb,pow(1.0-max(0,dot(normalDirection, viewDirection)),((_pulse_strength*sin(node_5016.b))*1.0+1.0)))+tex2D( _GrabTexture, sceneUVs.rg).rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
