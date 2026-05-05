using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 10f;
    public float zoomSpeed = 8f;
    public float minZoom = 4f;
    public float maxZoom = 20f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        HandlePan();
        HandleZoom();
    }

    void HandlePan()
    {
        if (Keyboard.current == null) return;

        Vector2 input = Vector2.zero;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) input.y += 1;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) input.y -= 1;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) input.x -= 1;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) input.x += 1;

        if (input == Vector2.zero) return;

        Vector3 forward = transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 motion = (forward * input.y + right * input.x) * panSpeed * Time.deltaTime;
        transform.position += motion;
    }

    void HandleZoom()
    {
        if (Mouse.current == null || cam == null) return;

        float scroll = Mouse.current.scroll.ReadValue().y;
        if (scroll == 0) return;

        cam.orthographicSize = Mathf.Clamp(
            cam.orthographicSize - scroll * zoomSpeed * 0.01f,
            minZoom,
            maxZoom
        );
    }
}
