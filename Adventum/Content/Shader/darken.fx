sampler s0;

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 Desaturate();
	}
}


float4 Desaturate(float2 coords: TEXCOORD0) : COLOR0
{
	float4 colour = tex2D(s0, coords);
	return colour.rgba * 0.5;
}