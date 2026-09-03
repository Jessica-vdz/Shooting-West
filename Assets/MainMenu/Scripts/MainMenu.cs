using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool start;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            start = true;
            SceneManager.LoadScene("Shootout", LoadSceneMode.Single);
        }
    }
}
