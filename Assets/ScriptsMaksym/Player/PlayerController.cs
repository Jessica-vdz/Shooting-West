using UnityEngine;

public class PlayerController : Entity
{
    public int playerID;
    [Header("Shooting")]
    [SerializeField] private Transform _gun;
    [SerializeField] private Transform _shootPoint;
    public float GunRotateSpeed;
    public float DamageAmount;
    public bool CanShoot;
    public LayerMask LayerMask;
    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            RotateGun(GunRotateSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            RotateGun(-GunRotateSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    public void RotateGun(float angle)
    {
        _gun.rotation *= new Quaternion(0, 0, angle * Time.deltaTime, 1);
    }
    public void Shoot()
    {
        if (CanShoot == false) return;

        var hit = Physics2D.Raycast(_shootPoint.position, _gun.right, 50, LayerMask);
        if (hit)
        {
            Debug.DrawLine(_shootPoint.position, hit.point, Color.red, 1);
            if (hit.collider.gameObject.TryGetComponent(out Entity e))
            {
                e.TakeDamage(DamageAmount);
            }
        }
        else
            Debug.DrawRay(_shootPoint.position, _gun.right * 50, Color.red, 1);
    }
    public override void OnTakeDamage()
    {
        if (IsDead() == false)
            GameManager.instance.RestartRound();
        else
        {
            int winnerID = playerID == 1 ? 2 : 1;
            GameManager.instance.EndGame(winnerID);
        }
    }
}
