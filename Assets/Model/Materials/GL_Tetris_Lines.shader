// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/GL_Tetris_Lines"
{
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        Pass
        {
            ZWrite On
            ZTest LEqual
            Cull Off
            
            Fog
            {
                Mode Off
            }
            BindChannels
            {
                Bind "vertex", vertex Bind "color", color
            }
        }
    }
}