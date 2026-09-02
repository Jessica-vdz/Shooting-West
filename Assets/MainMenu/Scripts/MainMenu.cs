using UnityEngine;

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
        }
    }
}
