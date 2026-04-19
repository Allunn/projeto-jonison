using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    private Camera cameraMain;

    private PlayerMovement movement;

    private float angle;
    private float recoilForce = 24f;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPoint;

    private float shootTimerMax = 0.3f;
    private float shootTimer = 0f;

    private int currentBullets = 1;
    void Start()
    {
        cameraMain = Camera.main;
        movement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        UpdateRotation();
     
        if (Input.GetKeyDown(KeyCode.Mouse0) && shootTimer <= 0f)
        {
            FireCannon();
        }

        shootTimer -= Time.deltaTime;

        if (movement.isGrounded) currentBullets = 1;
    }

    void FireCannon()
    {
        if (currentBullets <= 0) return;

        shootTimer = shootTimerMax;

        Vector2 fireDirection = transform.up;
        movement.ApplyCannonRecoil(fireDirection, recoilForce);

        var bulletGO = Instantiate(bullet, shootPoint.position, transform.rotation, null);
        bulletGO.GetComponent<Rigidbody2D>().linearVelocity = -bulletGO.transform.up * 10f;
        currentBullets--;
    }

    void UpdateRotation()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;

        Vector3 objectPos = cameraMain.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        angle = (Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg) + 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
