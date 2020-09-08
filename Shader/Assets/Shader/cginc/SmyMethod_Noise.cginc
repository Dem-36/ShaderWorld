//GradientNoise
float2 GradientNoise_dir(float2 p){
    p = p % 289;
    float x = (34 * p.x + 1) * p.x % 289 + p.y;
    x = (34 * x + 1) * x % 289;
    x = frac(x / 41) * 2 - 1;
    return normalize(float2(x - floor(x + 0.5),abs(x) - 0.5));
}

float GradientNoise(float2 p)
{
    float2 ip = floor(p);
    float2 fp = frac(p);
    float d00 = dot(GradientNoise_dir(ip), fp);
    float d01 = dot(GradientNoise_dir(ip + float2(0, 1)), fp - float2(0, 1));
    float d10 = dot(GradientNoise_dir(ip + float2(1, 0)), fp - float2(1, 0));
    float d11 = dot(GradientNoise_dir(ip + float2(1, 1)), fp - float2(1, 1));
    fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
    return lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x);
}

void GradientNoise_Float(float2 UV, float Scale, out float Out)
{
    Out = GradientNoise(UV * Scale) + 0.5;
}

//Noise/Random

float rand(float2 co)
{
	return frac(sin(dot(co.xy,float2(12.9898f,78.233)))*43758.5453);
}

float rand(float3 co)
{
	return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 56.787))) * 43758.5453);
}

float noise(float3 pos)
{
    float3 ip = floor(pos);
    float3 fp = smoothstep(0, 1, frac(pos));
    float4 a = float4(
        rand(ip + float3(0, 0, 0)),
        rand(ip + float3(1, 0, 0)),
        rand(ip + float3(0, 1, 0)),
        rand(ip + float3(1, 1, 0)));
    float4 b = float4(
        rand(ip + float3(0, 0, 1)),
        rand(ip + float3(1, 0, 1)),
        rand(ip + float3(0, 1, 1)),
        rand(ip + float3(1, 1, 1)));
    a = lerp(a, b, fp.z);
    a.xy = lerp(a.xy, a.zw, fp.y);
    return lerp(a.x, a.y, fp.x);
}

float perlin(float3 pos) {
    return  (noise(pos) * 32 +
            noise(pos * 2 ) * 16 +
            noise(pos * 4) * 8 +
            noise(pos * 8) * 4 +
            noise(pos * 16) * 2 +
            noise(pos * 32) ) / 63;
}

fixed2 random2(fixed2 st) {
	st = fixed2(dot(st, fixed2(127.1, 311.7)),
		dot(st, fixed2(269.5, 183.3)));
	return -1.0 + 2.0*frac(sin(st)*43758.5453123);
}

float perlinNoise(fixed2 st)
{
	fixed2 p = floor(st);
	fixed2 f = frac(st);
	fixed2 u = f * f*(3.0 - 2.0*f);

	float v00 = random2(p + fixed2(0, 0));
	float v10 = random2(p + fixed2(1, 0));
	float v01 = random2(p + fixed2(0, 1));
	float v11 = random2(p + fixed2(1, 1));

	return lerp(lerp(dot(v00, f - fixed2(0, 0)), dot(v10, f - fixed2(1, 0)), u.x),
		lerp(dot(v01, f - fixed2(0, 1)), dot(v11, f - fixed2(1, 1)), u.x),
		u.y) + 0.5f;
}

float2 Voronoi_Noise_RandomVector(float2 uv,float offset)
{
    float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
    uv = frac(sin(mul(uv, m)) * 46839.32);
    return float2(sin(uv.y*+offset)*0.5+0.5, cos(uv.x*offset)*0.5+0.5);
}

void Voronoi_Float(float2 uv,float angleOffset,float cellDensity,out float Out,out float cells)
{
    float2 g = floor(uv * cellDensity);
    float2 f = frac(uv * cellDensity);
    float t = 8.0;
    float3 res = float3(8.0, 0.0, 0.0);

    for(int y=-1; y<=1; y++)
    {
        for(int x=-1; x<=1; x++)
        {
            float2 lattice = float2(x,y);
            float2 offset = Voronoi_Noise_RandomVector(lattice + g, angleOffset);
            float d = distance(lattice + offset, f);
            if(d < res.x)
            {
                res = float3(d, offset.x, offset.y);
                Out = res.x;
                cells = res.y;
            }
        }
    }
}

void RadialShader_Float(float2 uv,float2 center,float strength,float offset,out float2 Out)
{
    float2 delta = uv - center;
    float delta2 = dot(delta.xy, delta.xy);
    float2 delta_offset = delta2 * strength;
    Out = uv + float2(delta.y, -delta.x) * delta_offset + offset;
}