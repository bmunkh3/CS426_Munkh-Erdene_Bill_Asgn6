using System.Collections;
using System.Data;
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
    private Light doorLight;
    private FirstPersonController firstPersonController;
    public Transform armatureAxis;
    public float rotOffset = -30f;
    public float rotRange = 30f;

    public void runPickup(float pickupTime, opencloseDoor newsDoor, Light light)
    {
        doorLight = light;
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
            doorLight.color = Color.blue;
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
        firstPersonController = GetComponent<FirstPersonController>();
        Debug.Log("Player: "+firstPersonController);

        if (news != null)
        {
            news.SetActive(false);
        }
    }

    void Update()
    {
        if (firstPersonController != null && armatureAxis != null)
        {
            float input = firstPersonController.getPitch();
            float t = Mathf.InverseLerp(-50f, 50f, input);
            float map = Mathf.Lerp(-rotRange, (rotRange-rotOffset), t);
            map += rotOffset;

            armatureAxis.localRotation = Quaternion.Euler(map, 0, 0);
        }
    }
}
