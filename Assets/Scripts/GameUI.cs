using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public GathererController[] gatherers;
    public TextMeshProUGUI resourceText;
    public TextMeshProUGUI stateText;

    void Update()
    {
        if (gatherers == null || gatherers.Length == 0) return;

        int total = 0;
        foreach (var g in gatherers)
        {
            if (g != null) total += g.totalDelivered;
        }

        if (resourceText != null)
        {
            resourceText.text = "Resources: " + total;
        }

        if (stateText != null && gatherers[0] != null)
        {
            stateText.text = "Drone 1: " + StateLabel(gatherers[0].state);
            if (gatherers.Length > 1 && gatherers[1] != null)
            {
                stateText.text += "\nDrone 2: " + StateLabel(gatherers[1].state);
            }
        }
    }

    string StateLabel(GathererController.State s)
    {
        switch (s)
        {
            case GathererController.State.Idle: return "Idle";
            case GathererController.State.GoingToResource: return "Going";
            case GathererController.State.Gathering: return "Gathering";
            case GathererController.State.ReturningToBase: return "Returning";
            default: return "?";
        }
    }
}
