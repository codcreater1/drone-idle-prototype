using UnityEngine;

public class GathererController : MonoBehaviour
{
    public Transform basePoint;
    public float speed = 3f;
    public float gatherTime = 2f;
    public int capacity = 3;

    public int totalDelivered = 0;

    public enum State { Idle, GoingToResource, Gathering, ReturningToBase }
    public State state = State.Idle;

    private Resource targetResource;
    private int carrying = 0;
    private float gatherTimer = 0f;

    void Update()
    {
        UpdateState();
    }

    public void AssignResource(Resource res)
    {
        targetResource = res;
        if (state == State.Idle)
        {
            state = State.GoingToResource;
        }
    }

    void UpdateState()
    {
        switch (state)
        {
            case State.Idle:
                break;

            case State.GoingToResource:
                if (targetResource == null)
                {
                    state = carrying > 0 ? State.ReturningToBase : State.Idle;
                    return;
                }

                Vector3 resPos = new Vector3(
                    targetResource.transform.position.x,
                    transform.position.y,
                    targetResource.transform.position.z
                );
                transform.position = Vector3.MoveTowards(
                    transform.position, resPos, speed * Time.deltaTime
                );

                if (Vector3.Distance(transform.position, resPos) < 0.1f)
                {
                    state = State.Gathering;
                    gatherTimer = 0f;
                }
                break;

            case State.Gathering:
                if (targetResource == null)
                {
                    state = carrying > 0 ? State.ReturningToBase : State.Idle;
                    return;
                }

                gatherTimer += Time.deltaTime;
                if (gatherTimer >= gatherTime)
                {
                    carrying += targetResource.Gather(capacity);
                    state = State.ReturningToBase;
                }
                break;

            case State.ReturningToBase:
                Vector3 basePos = new Vector3(
                    basePoint.position.x,
                    transform.position.y,
                    basePoint.position.z
                );
                transform.position = Vector3.MoveTowards(
                    transform.position, basePos, speed * Time.deltaTime
                );

                if (Vector3.Distance(transform.position, basePos) < 0.5f)
                {
                    totalDelivered += carrying;
                    carrying = 0;

                    if (targetResource != null)
                    {
                        state = State.GoingToResource;
                    }
                    else
                    {
                        state = State.Idle;
                    }
                }
                break;
        }
    }
}
