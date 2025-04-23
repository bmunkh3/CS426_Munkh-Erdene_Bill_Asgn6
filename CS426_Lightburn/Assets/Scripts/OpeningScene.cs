using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScene : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("Building");
    }
}
