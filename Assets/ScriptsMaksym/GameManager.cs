using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Round Timer")]
    [SerializeField] private float _timerMinRange;
    [SerializeField] private float _timerMaxRange;
    [Header("Round End Random Spread")]
    [SerializeField] private float _randomXSpread;
    [SerializeField] private float _randomYSpread;
    [Header("Player References")]
    [SerializeField] private PlayerController _player1;
    [SerializeField] private PlayerController _player2;

    private Vector3 player1OriginalPos;
    private Vector3 player2OriginalPos;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        player1OriginalPos = _player1.transform.position;
        player2OriginalPos = _player2.transform.position;
    }
    public void StartTimer()
    {
        float time = Random.Range(_timerMinRange, _timerMaxRange);
        StartCoroutine(Timer(time));
    }
    private IEnumerator Timer(float seconds)
    {
        Debug.Log("Round starts in " + seconds);
        yield return new WaitForSeconds(seconds);
        RoundStart();
    }
    public void EndGame(int winner)
    {
        Debug.Log("GAME END, PLAYER " + winner + " WON!!!!!");
    }
    public void RoundStart()
    {
        Debug.Log("ROUND START");
        float randomY1 = Random.Range(-_randomYSpread, _randomYSpread);
        float randomX1 = Random.Range(-_randomXSpread, _randomXSpread);
        ManagePlayer(_player1, true, true, new Vector3(randomX1, randomY1), player1OriginalPos);

        float randomY2 = Random.Range(-_randomYSpread, _randomYSpread);
        float randomX2 = Random.Range(-_randomXSpread, _randomXSpread);
        ManagePlayer(_player2, true, true, new Vector3(randomX2, randomY2), player2OriginalPos);
    }
    public void RestartRound()
    {
        Debug.Log("ROUND END");
        ManagePlayer(_player1, false, false, _player1.transform.position);
        ManagePlayer(_player2, false, false, _player2.transform.position);
        StartTimer();
    }
    public void ManagePlayer(PlayerController player, bool enabled, bool canShoot, Vector3 newPos, Vector3 originalPos = default)
    {
        player.CanShoot = canShoot;
        player.transform.position = originalPos + newPos;
        player.gameObject.SetActive(enabled);
    }
}
