using SojaExiles;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PickupNews : MonoBehaviour
{
    public Score scoreManager;

    public bool pickupCond = true;
    private float proximity = 3f;
    bool debug = false;
    public Light hoverLight;
    public GameObject player;
    private bool mouseOver = false;
    private PlayerAnimationController playerAnimationController;
    private float waitDestroy = 1.2f;
    public opencloseDoor door;
    public Light doorLight;
    private bool pickedUp = false;
    private int currFires = 0; 
    private int totalFires = 0;
    public TextMeshPro firesText;

    public Camera playerCamera;
    public GameObject[] firstFloorFires;
    private Material[] mats;
    public Shader shader;


    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        mats = renderer.materials;
        mats[1].color = Color.white;

        playerAnimationController = player.GetComponent<PlayerAnimationController>();
        
        if (door != null){
            door.locked = true;
        }
        toggleLights(true);
        foreach (GameObject fire in firstFloorFires)
        {
            if (fire != null && fire.activeInHierarchy)
            {
                totalFires++;
            }
        }
        currFires = totalFires;
        firesText.text = "Indoor Fires: "+ currFires + "/" + totalFires;
    }

    void toggleLights (bool toggle)
    {
        if (doorLight != null) {
            doorLight.enabled = toggle;
        }
        if (hoverLight != null){
            hoverLight.enabled = toggle;
        }
    }
    void changeColor(Color color)
    {
        toggleLights(true);
        if (doorLight != null){
            doorLight.color = color;
        }
        if (hoverLight != null){
            mats[1].color = color;
            hoverLight.color = color;
        }

    }

    void PickUp()
    {
        if (scoreManager != null)
        {
            scoreManager.AddNewsPoint();
        }

        Destroy(gameObject, waitDestroy);
        playerAnimationController.runPickup(waitDestroy,door,doorLight);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCamera != null &&  firesText != null)
        {
            Vector3 dir = firesText.transform.position - playerCamera.transform.position;
            dir.y = 0f;
            firesText.transform.rotation = Quaternion.LookRotation(dir);
        }

        pickupCond = firesCleared();
        if (pickedUp)
        {
            changeColor(Color.cyan);
        }

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (debug) { Debug.Log("Distance: " + distance + " | Mouse Over: " + mouseOver);}
            if ((distance <= proximity) || door.inDist)
            {
                firesText.enabled = true;
                if (pickupCond)
                {
                    changeColor(Color.blue);
                }else{
                    changeColor(Color.red);
                }
            }else if (!pickedUp){
                firesText.enabled = false;
                changeColor(Color.white);
            }

            if (distance <= proximity && pickupCond && Input.GetKeyDown(KeyCode.E))
            {
                firesText.enabled = false;
                Vector3 dir = transform.position - player.transform.position;
                dir.y = 0f;
                if (dir != Vector3.zero && Vector3.Angle(player.transform.forward, dir) > 10f) {
                    player.transform.rotation = Quaternion.LookRotation(dir);
                }

                
                Debug.Log("Newspaper: Picked Up");
                PickUp();
            }
        }
    }

    bool firesCleared() 
    {

        currFires = 0;
        bool temp = true;
        foreach (GameObject fire in firstFloorFires)
        {
            if (fire != null && fire.activeInHierarchy)
            {
                temp = false;
                currFires++;
            }
        }
        Debug.Log(currFires);
        firesText.text = "Floor Fires: " + currFires + "/" + totalFires;
        return temp;
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
