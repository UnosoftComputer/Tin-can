using UnityEngine;


public class HeightCarver : MonoBehaviour
{
    [SerializeField] private ComputeShader carveShader;
    [SerializeField] private Texture2D initialHeightMap;
    [SerializeField] private Renderer targetRenderer;  
    

    [Header("Параметры кисти")]
    [SerializeField] private float radius = 0.15f;  
    [SerializeField] private float depth = 0.05f;  

    RenderTexture _rt;
    int _kernel;

    private void Awake()
    {
        _kernel = carveShader.FindKernel("Carve");

        _rt = new RenderTexture(
            initialHeightMap.width, 
            initialHeightMap.height,
            0, 
            RenderTextureFormat.ARGBFloat);
        _rt.enableRandomWrite = true;  
        _rt.filterMode = FilterMode.Bilinear;
        _rt.Create();
        Graphics.Blit(initialHeightMap, _rt);
        targetRenderer.material.mainTexture = _rt;
    }

    public void ApplyForce(Vector2 pos)
    {
        carveShader.SetTexture(_kernel, "HeightTex", _rt);
        carveShader.SetFloats("_Center", pos.x, pos.y);
        carveShader.SetFloat("_Radius", radius);
        carveShader.SetFloat("_Strength", depth);
        carveShader.SetFloat("_Aspect", (float)_rt.width / _rt.height);

        carveShader.Dispatch(_kernel,
            Mathf.CeilToInt(_rt.width / 8f),
            Mathf.CeilToInt(_rt.height / 8f), 1);
    }

    private void OnDestroy()
    {
        if (_rt) { _rt.Release(); Destroy(_rt); }
    }
}
