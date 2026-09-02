using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private float _timerMinRange;
    [SerializeField] private float _timerMaxRange;
    [SerializeField] private PlayerController _player1;
    [SerializeField] private PlayerController _player2;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    public void StartTimer()
    {
        float time = Random.Range(_timerMinRange, _timerMaxRange);
        Debug.Log("Round starts in " + time);
        StartCoroutine(Timer(time));
    }
    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        RoundStart();
    }
    public void RoundStart()
    {
        Debug.Log("ROUND START");
        _player1.CanShoot = true;
        _player2.CanShoot = true;
    }
    public void RoundEnd(bool startAnotherRound)
    {
        Debug.Log("ROUND END");
        _player1.CanShoot = false;
        _player2.CanShoot = false;
        if (startAnotherRound)
            StartTimer();
    }
}
