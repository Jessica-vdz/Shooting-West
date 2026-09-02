using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private float _timerMinRange;
    [SerializeField] private float _timerMaxRange;
    [SerializeField] private float _randomYSpread;
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
        ManagePlayer(_player1, true, true, new Vector3(_player1.transform.position.x, randomY1));

        float randomY2 = Random.Range(-_randomYSpread, _randomYSpread);
        ManagePlayer(_player2, true, true, new Vector3(_player2.transform.position.x, randomY2));
    }
    public void RoundEnd(bool startAnotherRound)
    {
        Debug.Log("ROUND END");
        if (_player1.IsDead())
        {
            EndGame(2);
            return;
        }
        else if (_player2.IsDead())
        {
            EndGame(1);
            return;
        }
            ManagePlayer(_player1, false, false, _player1.transform.position);
        ManagePlayer(_player2, false, false, _player2.transform.position);
        if (startAnotherRound)
            StartTimer();
    }
    public void ManagePlayer(PlayerController player, bool enabled, bool canShoot, Vector3 newPos)
    {
        player.CanShoot = canShoot;
        player.transform.position = newPos;
        player.gameObject.SetActive(enabled);
    }
}
