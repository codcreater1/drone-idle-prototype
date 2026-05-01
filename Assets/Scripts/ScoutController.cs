using UnityEngine;
using UnityEngine.InputSystem;

public class ScoutController : MonoBehaviour
{
    public float speed = 5f;

    private Vector3 targetPosition;
    private bool hasTarget = false;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mouseScreen = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mouseScreen);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.name == "Ground")
                {
                    targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    hasTarget = true;
                }
            }
        }

        if (hasTarget)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                hasTarget = false;
            }
        }
    }
}
