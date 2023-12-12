using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{

    public GameObject Grenade;
    public Transform GrenadeSpot;
    public float throwForce;
    public float fireRate = 0.5f;
    public Camera fpsCam;



    private float nextTimetoFire = 0f;
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject grenade = Instantiate (Grenade, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
