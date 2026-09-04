using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerID;
    [Header("Shooting")]
    [SerializeField] private Transform _gun;
    [SerializeField] private Transform _shootPoint;
    public float GunRotateSpeed;
    public float DamageAmount;
    public bool CanShoot;
    public LayerMask LayerMask;

    private string lastAngle;
    private void Awake()
    {
        readDatafromSerialPort.Player2Pressed += Shoot;
    }
    public void Shoot(string currentAngle)
    {
        if (currentAngle == lastAngle) return;
        lastAngle = currentAngle;
        float.TryParse(currentAngle, out float r);
        Debug.Log("TRYING TO SHOOT");
        if (CheckPointer(r, 0, 5f))
        {
            Debug.Log("HIT");
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
