using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class FiremanNPCAI : MonoBehaviour
{
    public List<GameObject> targets;
    private Transform target;
    private int listSize = 0;
    private int targetIter = 0;

    public float stopDistance = 1f;
    public float rotSpeed = 5f;

    private Animator animator;
    private NavMeshAgent agent;

    public float shootDuration = 3f;
    private float shootTimer = 0f;
    private bool isShooting = false;

    public ParticleSystem particles;
    public float sprayDistance = 8f;
    private ParticleSystem.MainModule main;
    public AudioSource hoseSource;


    void toggleHose(bool toggle)
    {
        if (toggle)
        {
            main.startSpeed = sprayDistance;
            if (hoseSource != null)
            {
                hoseSource.Play();
            }
            particles.Play();
        }
        else
        {
            if (hoseSource != null)
            {
                hoseSource.Pause();
            }
            particles.Stop();
        }
    }

    void Start()
    {
        listSize = targets.Count;
        if (listSize > 0)
        {
            target = targets[targetIter].transform;
        }
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.stoppingDistance = stopDistance;

        if (particles != null)
        {
            particles.Stop(true);
            main = particles.main;
            main.startSpeed = sprayDistance;
        }
    }

    void Update()
    {
        if (targets == null) return;

        if (target == null)
        {
            agent.isStopped = true;
            animator.SetBool("Shoot", false);
            toggleHose(false);
            animator.SetBool("Move", false);
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);
        Vector3 dir = (transform.position - target.position);
        dir.y = 0f;
        Quaternion lookRot = Quaternion.LookRotation(-dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotSpeed);
        float angle = Vector3.Angle(transform.forward, -dir);

        if (distance > stopDistance || angle > 15f)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            animator.SetBool("Move", true);
        }else if (!isShooting)
        {
            agent.isStopped = true;
            animator.SetBool("Move", false);

            
            isShooting = true;
            
            shootTimer = 0f;
            animator.SetBool("Shoot", true);
            toggleHose(true);
        }

        if (isShooting)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootDuration)
            {
                isShooting = false;
                shootTimer = 0f;
                toggleHose(false);
                animator.SetBool("Shoot", false);
                targetIter++;
                if (targetIter >= listSize)
                {
                    targetIter = 0;
                }
                if (targets[targetIter] != null)
                {
                    target = targets[targetIter].transform;
                }
            }
        }
    }
}
