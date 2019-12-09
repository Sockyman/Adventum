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
	float4 grayR = colour;
	grayR.gb = grayR.r;
	float4 grayG = colour;
	grayG.rb = grayG.g;
	float4 grayB = colour;
	grayB.rg = grayB.b;
	return (grayR * 0.3 + grayG * 0.59 + grayB * 0.11) * 0.1 + colour * 0.9;
}

