using UnityEngine;
using UnityEngine.AI;

public class Rescue : MonoBehaviour
{
    private float proximity = 5f;
    bool debug = false;
    public GameObject hoverLight;
    public Transform player;
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
        if (other.CompareTag("safeZone"))
        {
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
            hoverLight.SetActive(false);

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

        float distance = Vector3.Distance(transform.position, player.position);
        if (following)
        {
            hoverLight.SetActive(false);
            if (distance > followDistance){
                float distanceMoved = Vector3.Distance(player.position, lastTargetPosition);
                if (distanceMoved > updateThreshold)
                {
                    agent.isStopped = false;
                    agent.SetDestination(player.position);
                    lastTargetPosition = player.position;
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


                if (distance <= proximity && mouseOver)
                {
                    hoverLight.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.P) && agent != null)
                    {
                        if (animator != null)
                        {
                            animator.SetTrigger("Run");
                        }
                        following = true;
                    }
                }
                else
                {
                    hoverLight.SetActive(false);

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
