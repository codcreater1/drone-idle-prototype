using UnityEngine;

public class Resource : MonoBehaviour
{
    public int amount = 10;

    private int startAmount;
    private Vector3 startScale;

    void Start()
    {
        startAmount = amount;
        startScale = transform.localScale;
    }

    public int Gather(int request)
    {
        int taken = Mathf.Min(request, amount);
        amount -= taken;

        float ratio = Mathf.Max(0.2f, (float)amount / startAmount);
        transform.localScale = startScale * ratio;

        if (amount <= 0)
        {
            Destroy(gameObject);
        }

        return taken;
    }
}
