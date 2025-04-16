using SojaExiles;
using Unity.VisualScripting;
using UnityEngine;

public class PickupNews : MonoBehaviour
{
    private bool pickupCond = false;
    private float proximity = 3f;
    bool debug = false;
    Light hoverLight;
    public GameObject player;
    private bool mouseOver = false;
    private PlayerAnimationController playerAnimationController;
    private float waitDestroy = 1.2f;
    public opencloseDoor door;

    public GameObject[] firstFloorFires;

    void Start()
    {
        playerAnimationController = player.GetComponent<PlayerAnimationController>();
        hoverLight = GetComponent<Light>();
        if (door != null)
        {
            door.locked = true;
        }
        if (hoverLight != null)
        {
            hoverLight.enabled = false;
        }
    }

    void PickUp()
    {
        Destroy(gameObject, waitDestroy);
        playerAnimationController.runPickup(waitDestroy,door);
    }

    // Update is called once per frame
    void Update()
    {
        pickupCond = firesCleared();

        if (hoverLight != null)
        {
            if (pickupCond){
                hoverLight.color = Color.blue;
            }
            else{
                hoverLight.color = Color.red;
            }
        }

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (debug){ Debug.Log("Distance: " + distance + " | Mouse Over: " + mouseOver);}

            if (distance <= proximity && mouseOver){
                hoverLight.enabled = true;
                if (pickupCond){
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        Debug.Log("Newspaper: Picked Up");
                        PickUp();
                    }
                }
            }
            else{
                hoverLight.enabled = false;
            }
        }
    }

    bool firesCleared() 
    {
        foreach (GameObject fire in firstFloorFires)
        {
            if (fire != null && fire.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
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
