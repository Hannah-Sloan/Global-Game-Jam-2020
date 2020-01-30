using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class CameraFlag : MonoBehaviour
{
    [SerializeField] private DepthTextureMode depthTextureMode = DepthTextureMode.None;

    [ContextMenu("Refresh")]
    void OnEnable()
    {
        //Request camera to generate additional texture for shader effects.
        GetComponent<Camera>().depthTextureMode = depthTextureMode;
    }
}
