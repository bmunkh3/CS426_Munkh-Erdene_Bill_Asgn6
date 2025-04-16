using System.Collections;
using SojaExiles;
using Unity.Jobs;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public GameObject hose;
    public GameObject news;
    private float waitPickUp = 1f;
    private float holdTime = 5f;
    private AudioSource newsSource;
    public AudioClip newsClip;
    private AudioSource playerSource;
    public AudioClip equip;
    private opencloseDoor door;

    public void runPickup(float pickupTime, opencloseDoor newsDoor)
    {
        door = newsDoor;
        waitPickUp = pickupTime;
        hose.SetActive(false);
        if (playerSource != null)
        {
            playerSource.PlayOneShot(equip);
        }

        if (animator != null)
        {
            animator.SetBool("Pickup",true);
        }
        StartCoroutine(pickupRoutine());
        StartCoroutine(finishPickupRoutine());
        
    }
    IEnumerator pickupRoutine()
    {
        yield return new WaitForSeconds(waitPickUp);
        if (door != null)
        {
            door.openDoor();
        }
        news.SetActive(true);
        if (newsSource != null && newsClip != null)
        {
            newsSource.PlayOneShot(newsClip);
        }
    }

    IEnumerator finishPickupRoutine()
    {
        yield return new WaitForSeconds(holdTime);
        news.SetActive(false);
        animator.SetBool("Pickup", false);
        hose.SetActive(true);
        if (playerSource != null)
        {
            playerSource.PlayOneShot(equip);
        }
    }

    void Start()
    {
        playerSource = GetComponent<AudioSource>();
        newsSource = news.GetComponent<AudioSource>();
        if (news != null)
        {
            news.SetActive(false);
        }
    }
}
