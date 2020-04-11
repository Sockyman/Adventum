sampler s0;  
		
texture lightMask;  
sampler lightSampler = sampler_state{Texture = (lightMask);};
      
float4 PixelShaderLight(float2 coords: TEXCOORD0) : COLOR0  
{  
    float4 color = tex2D(s0, coords);  
    float4 lightColor = tex2D(lightSampler, coords);  
    

    if (lightColor.r > 0.75)
    {
        return color;
    }
    else if (lightColor.r > 0.55) 
    {
        return 0.75 * color;
    }
    else if (lightColor.r > 0.40)
    {
        return 0.55 * color;
    }
    else if (lightColor.r > 0.20)
    {
        return 0.40 * color;
    }
    return 0.1 * color;
}  

	      
technique Technique1  
{  
    pass Pass1  
    {  
        PixelShader = compile ps_2_0 PixelShaderLight();  
    }  
}  