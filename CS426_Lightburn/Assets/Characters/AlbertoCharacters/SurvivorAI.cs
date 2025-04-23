using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SurvivorAI : MonoBehaviour
{
    public float detectionRadius = 10f;
    public Transform safeZone;

    private Animator animator;
    private NavMeshAgent agent;
    private GameObject player;

    private bool isRunning = false;
    private bool waitingToRun = false;
    private bool hasRun = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Start in terrified idle pose
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Safe");
    }

    void Update()
    {
        GameObject fire = FindClosestFire();

        float distToFire = fire ? Vector3.Distance(transform.position, fire.transform.position) : Mathf.Infinity;
        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Start running if fire is near and not already running
        if (fire && distToFire < detectionRadius)
        {
            if (!isRunning && !waitingToRun && !hasRun && agent.isOnNavMesh)
            {
                StartCoroutine(WaitThenRun());
            }
        }
        // Handle arrival at safe zone
        else if (!agent.pathPending && agent.remainingDistance < 0.5f && isRunning)
        {
            StartCoroutine(HandleSafeArrival());
        }
        // Optional: React to player if nearby and not running
        else if (distToPlayer < 5f && !isRunning && !waitingToRun)
        {
            agent.ResetPath(); // Stop movement
        }
    }

    GameObject FindClosestFire()
    {
        GameObject[] fires = GameObject.FindGameObjectsWithTag("Fire");
        GameObject closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject f in fires)
        {
            float dist = Vector3.Distance(transform.position, f.transform.position);
            if (dist < minDist)
            {
                closest = f;
                minDist = dist;
            }
        }

        return closest;
    }

    void BeginRun()
    {
        isRunning = true;
        hasRun = true;
        animator.SetTrigger("Run"); //  Trigger the run animation

        if (agent.isOnNavMesh)
        {
            agent.SetDestination(safeZone.position);
        }
    }

    IEnumerator WaitThenRun()
    {
        waitingToRun = true;

        // Stay terrified before running
        yield return new WaitForSeconds(2f);

        BeginRun();
        waitingToRun = false;
    }

    IEnumerator HandleSafeArrival()
    {
        isRunning = false;

        // Let them cool down in terrified state
        yield return new WaitForSeconds(3f);

        animator.SetTrigger("Safe"); // Trigger back to idle/terrified
        agent.ResetPath();
    }
}
