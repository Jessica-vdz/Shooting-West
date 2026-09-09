using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // most of the code here is made by theo
    public bool start;

    void Update() { 
        StartGame();
    }

    void StartGame()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("space");
            start = true;
            SceneManager.LoadScene("despa", LoadSceneMode.Single);
        }
    }
}
