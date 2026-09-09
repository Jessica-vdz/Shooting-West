using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // most of all the code here is made by Maksym
    public int playerID;
    [Header("Shooting")]
    //[SerializeField] private Transform _gun;
    //[SerializeField] private Transform _shootPoint;
    //public float GunRotateSpeed;
    //public float DamageAmount;
    public bool CanShoot;
    public LayerMask LayerMask;
    public int P1Score = 0;
    public int P2Score = 0;

    private string lastAngle;
    private void Awake()
    {
        readDatafromSerialPort.Player1Pressed += Shoot;
        readDatafromSerialPort.Player2Pressed += Shoot;
    }
    public void Shoot(string currentAngle, string playerid)
    {
        Debug.Log(currentAngle);
        if (!CanShoot) return;
        //if (currentAngle == lastAngle) return;
        //lastAngle = currentAngle;
        float.TryParse(currentAngle, out float r);
        Debug.Log("TRYING TO SHOOT");
        if (CheckPointer(r, 5, 12f))
        {
            Debug.Log("HIT");
            takeDamage(playerid);
        }
        else
        {
            Debug.Log("MISS");
        }

    }
   
    public static bool CheckPointer(float current, float needed, float allowed)
    {
        float min = current - allowed;
        float max = current + allowed;

        if(needed < min) return false;
        if(needed > max) return false;
        return true;
    }
    void takeDamage(string playerid)
    {
        bool p1point = false;
        bool p2point = false;
        if(playerid == "Player1" && !p2point)
        {
            p1point = true;
            P1Score++;
            if (P1Score >= 3)
            {
                GameManager.instance.GameWon(1, P1Score, P2Score);
                return;
            }
            Debug.Log($"score Player 1: {P1Score}, score Player 2 {P2Score}");
            GameManager.instance.RestartRound();
        }
        if(playerid == "Player2" && !p1point)
        {
            p2point = true;
            P2Score++;
            if(P2Score >= 3)
            {
                GameManager.instance.GameWon(2,P2Score,P1Score);
                return;
            }
            Debug.Log($"score Player 1: {P1Score}, score Player 2 {P2Score}");
            GameManager.instance.RestartRound();
        }
        
    }

    //public override void OnTakeDamage()
    //{
    //    if (IsDead() == false)
    //        GameManager.instance.RestartRound();
    //    else
    //    {
    //        int winnerID = playerID == 1 ? 2 : 1;
    //        GameManager.instance.EndGame(winnerID);
    //    }
    //}
}
