using UnityEngine;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range  = 100f;
    public float fireRate = 10f;
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
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDmg(damage);
            }
        }

    }
}
