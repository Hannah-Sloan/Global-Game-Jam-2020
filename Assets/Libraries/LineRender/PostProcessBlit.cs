using UnityEngine;

[ExecuteAlways]
public class PostProcessBlit : MonoBehaviour
{
    [SerializeField] private Material materialToRenderWith;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(materialToRenderWith)
            Graphics.Blit(source, destination, materialToRenderWith);
    }
}
