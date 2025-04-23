// add score manager
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// access the Text Mesh Pro namespace
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text fireText;
    public TMP_Text survivorText;
    public TMP_Text newsText;
    public int maxSurvivorScore = 4;
    public int maxNewsScore = 5;

    int fireScore = 0;
    int survivorScore = 0;
    int newsScore = 0;
    bool newsComplete = false;
    bool fireComplete = false;
    bool rescueComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        UpdateFireText();
        UpdateSurvivorText();
        UpdateNewsText();
    }

    // update will exponentially increase fire limit base on fire objects on game scene instead of having a set limit
    void Update()
    {
        UpdateFireText();
        if (fireComplete && newsComplete && rescueComplete){
            SceneManager.LoadScene("End");
        }
    }

    //we will call this method from our target script
    // whenever the player collides or shoots a target a point will be added
    public void AddFirePoint()
    {
        fireScore++;
    }

    public void AddSurvivorPoint()
    {
        survivorScore++;
        UpdateSurvivorText();
    }

    public void AddNewsPoint()
    {
        newsScore++;
        UpdateNewsText();
    }

    void UpdateFireText()
    {
        int liveFires = GameObject.FindGameObjectsWithTag("Fire").Length;
        int totalFires = fireScore + liveFires;

        if (liveFires == 0 && fireScore > 0){
            fireComplete = true;
            fireText.text = "All fires out!";
        }
        else {
            fireText.text = $"Fires: {fireScore} / {totalFires}";
        }
    }

    void UpdateSurvivorText()
    {
        if (survivorScore >= maxSurvivorScore){
            rescueComplete = true;
            survivorText.text = "All survivors safe!";
        }
        else{
            survivorText.text = $"Survivors: {survivorScore} / {maxSurvivorScore}";
        }
    }

    void UpdateNewsText()
    {
        newsComplete = true;
        if (newsScore >= maxNewsScore){
            newsText.text = "All news read!";
        }
        else{
            newsText.text = $"Newspapers: {newsScore} / {maxNewsScore}";
        }
    }
}