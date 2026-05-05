using UnityEngine;
using UnityEngine.InputSystem;

public class DroneManager : MonoBehaviour
{
    public GathererController[] gatherers;

    void Update()
    {
        if (Mouse.current == null) return;
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mouseScreen);

        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        Resource res = hit.collider.GetComponent<Resource>();
        if (res == null) return;

        GathererController target = FindIdleDrone();
        if (target == null)
        {
            target = FindNearestDrone(res.transform.position);
        }

        if (target != null)
        {
            target.AssignResource(res);
        }
    }

    GathererController FindIdleDrone()
    {
        foreach (var g in gatherers)
        {
            if (g != null && g.state == GathererController.State.Idle)
            {
                return g;
            }
        }
        return null;
    }

    GathererController FindNearestDrone(Vector3 point)
    {
        GathererController nearest = null;
        float minDist = float.MaxValue;

        foreach (var g in gatherers)
        {
            if (g == null) continue;
            float dist = Vector3.Distance(g.transform.position, point);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = g;
            }
        }

        return nearest;
    }
}
