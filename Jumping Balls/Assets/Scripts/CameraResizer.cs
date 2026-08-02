using UnityEngine;

[ExecuteInEditMode]
public class Unity6CameraResizer : MonoBehaviour
{
    // Define the ideal aspect ratio your level was designed for (e.g., 9:16 portrait)
    public float targetAspect = 9f / 16f; 
    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (mainCamera == null) return;

        // Calculate current simulator device aspect ratio
        float currentAspect = (float)Screen.width / Screen.height;

        if (mainCamera.orthographic)
        {
            // Base size you designed the game around (e.g., size 5)
            float defaultSize = 5f; 
            
            if (currentAspect < targetAspect)
            {
                // Screen is narrower than target, scale zoom out horizontally
                mainCamera.orthographicSize = defaultSize * (targetAspect / currentAspect);
            }
            else
            {
                // Screen is wider/taller, maintain standard scale
                mainCamera.orthographicSize = defaultSize;
            }
        }
    }
}
