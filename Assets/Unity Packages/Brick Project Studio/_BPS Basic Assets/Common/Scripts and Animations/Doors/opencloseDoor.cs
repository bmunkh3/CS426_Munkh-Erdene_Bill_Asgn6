using System.Collections;
using UnityEngine;

namespace SojaExiles
{
    public class opencloseDoor : MonoBehaviour
    {
        public Animator openandclose;
        public bool open;
        public Transform Player;
        public float interactionDistance = 3f; // how close player must be to interact
        public bool locked = false;

        public void openDoor()
        {
            if (!open)
            {
                StartCoroutine(opening());
            }
        }

        void Start()
        {
            open = false;
        }

        void Update()
        {
            
            if (Player)
            {
                float dist = Vector3.Distance(Player.position, transform.position);

                if (dist < interactionDistance && Input.GetKeyDown(KeyCode.E))
                {
                    if (!locked)
                    {
                        if (!open)
                        {
                            StartCoroutine(opening());
                        }
                        else
                        {
                            StartCoroutine(closing());
                        }
                    }
                    else
                    {
                        Debug.Log("Door is Locked");
                    }
                }
            }
        }

        IEnumerator opening()
        {
            Debug.Log("You are opening the door");
            openandclose.Play("Opening");
            open = true;
            yield return new WaitForSeconds(0.5f);
        }

        IEnumerator closing()
        {
            Debug.Log("You are closing the door");
            openandclose.Play("Closing");
            open = false;
            yield return new WaitForSeconds(0.5f);
        }
    }
}