using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    private Camera cameraMain;

    private PlayerMovement movement;

    private float angle;
    private float recoilForce = 25f;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPoint;

    private float shootTimerMax = 0.3f;
    private float shootTimer = 0f;
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
    }

    void FireCannon()
    {
        shootTimer = shootTimerMax;

        Vector2 fireDirection = transform.up;
        movement.ApplyCannonRecoil(fireDirection, recoilForce);

        var bulletGO = Instantiate(bullet, shootPoint.position, transform.rotation, null);
        bulletGO.GetComponent<Rigidbody2D>().AddForce(-bulletGO.transform.up * 300f);
    }

    void UpdateRotation()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;

        Vector3 objectPos = cameraMain.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        angle = (Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg) + 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
