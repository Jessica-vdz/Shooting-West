using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int ScoreNeededToWin;
    [HideInInspector] public int Score1;
    [HideInInspector] public int Score2;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void AddScore(int player)
    {
        if (player == 1)
        {
            Score1++;
        }
        else if (player == 2)
        {
            Score2++;
        }
    }
}
