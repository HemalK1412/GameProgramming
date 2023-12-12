using UnityEngine;

public class GunObsolete : MonoBehaviour
{

    public float damage = 10f;
    public float range  = 100f;
    public float fireRate = 10f;
    public Camera fpsCam;

    public GameObject MuzzleFlash;
    public Transform MuzzleFlashPosition;



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
        RaycastHit hit;
        
        GameObject Flash = Instantiate(MuzzleFlash, MuzzleFlashPosition.transform);
        Destroy (Flash, 0.2f);
        

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDmg(damage);
            }
        }

    }
}
