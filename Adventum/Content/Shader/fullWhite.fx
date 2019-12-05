sampler s0;

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 FullWhite();
	}
}


float4 FullWhite(float2 coords: TEXCOORD0) : COLOR0
{
	float4 colour = tex2D(s0, coords);
	return float4(1.0f, 1.0f, 1.0f, 0.0f);
}

