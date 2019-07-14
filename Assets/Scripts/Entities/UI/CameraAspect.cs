using UnityEngine;

// Modified from http://gamedesigntheory.blogspot.com/2010/09/controlling-aspect-ratio-in-unity.html
public class CameraAspect : MonoBehaviour
{
    public static readonly float TARGET_ASPECT = 4.0f / 3.0f;

    // Private state
    private float oldWindowAspect;

    // Use this for initialization
    void FixedUpdate()
    {
        // determine the game window's current aspect ratio
        float currentWindowAspect = (float)Screen.width / (float)Screen.height;

        if (currentWindowAspect != oldWindowAspect)
        {
            // current viewport height should be scaled by this amount
            float scaleHeight = currentWindowAspect / TARGET_ASPECT;

            // obtain camera component so we can modify its viewport
            Camera camera = GetComponent<Camera>();

            // if scaled height is less than current height, add letterbox
            if (scaleHeight < 1.0f)
            {
                Rect rect = camera.rect;

                rect.width = 1.0f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1.0f - scaleHeight) / 2.0f;

                camera.rect = rect;
            }
            else // add pillarbox
            {
                float scalewidth = 1.0f / scaleHeight;

                Rect rect = camera.rect;

                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;

                camera.rect = rect;
            }

            this.oldWindowAspect = currentWindowAspect;
        }
    }

}