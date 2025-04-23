using UnityEngine;
using UnityEngine.AI;

public class Rescue : MonoBehaviour
{
    public Score scoreManager;
    private bool rescued = false;

    private float proximity = 5f;
    bool debug = false;
    public Light hoverLight;
    public GameObject player;
    private bool mouseOver = false;
    private NavMeshAgent agent;
    private Animator animator;
    private bool following = false;
    private float followDistance = 3.5f;
    public Transform safeZone;
    private bool tooSafeZone = false;
    private float updateThreshold = 0.5f;
    private Vector3 lastTargetPosition;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sruvivor Running to Safety");
        if (!rescued && other.CompareTag("safeZone"))
        {
            rescued = true;
            scoreManager.AddSurvivorPoint();
            tooSafeZone = true;
        }
    }
    void Start()
    {
        lastTargetPosition = player.transform.position;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (hoverLight != null)
        {
            hoverLight.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (tooSafeZone && safeZone != null)
        {
            float safeDistance = Vector3.Distance(transform.position, safeZone.position);
            if (safeDistance > followDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(safeZone.position);
            }
            else
            {
                if (animator != null)
                {
                    animator.SetTrigger("Safe");
                }
                agent.ResetPath();
            }

            return;
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (following)
        {
            hoverLight.enabled = false;
            if (distance > followDistance){
                float distanceMoved = Vector3.Distance(player.transform.position, lastTargetPosition);
                if (distanceMoved > updateThreshold)
                {
                    agent.isStopped = false;
                    agent.SetDestination(player.transform.position);
                    lastTargetPosition = player.transform.position;
                }
            }
            else{
                agent.isStopped = true;
            }
        }
        else
        {
            if (player != null)
            {

                if (debug) { Debug.Log("Distance: " + distance + " | Mouse Over: " + mouseOver); }


                if (distance <= proximity)
                {
                    hoverLight.color = Color.blue;
                    if (Input.GetKeyDown(KeyCode.E) && agent != null)
                    {
                        hoverLight.enabled = false;
                        Vector3 dir = player.transform.position - transform.position;
                        dir.y = 0f;
                        if (dir != Vector3.zero && Vector3.Angle(player.transform.forward, dir) > 10f)
                        {
                            player.transform.rotation = Quaternion.LookRotation(-dir);
                        }
                        if (animator != null)
                        {
                            animator.SetTrigger("Run");
                        }
                        following = true;
                    }
                }
                else
                {
                    hoverLight.color = Color.white;
                }
            }
        }
    }

    void OnMouseEnter()
    {
        mouseOver = true;
    }
    void OnMouseExit()
    {
        mouseOver = false;
    }
}
