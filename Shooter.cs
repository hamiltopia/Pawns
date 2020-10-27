using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float fireRate = .2f;
    public int damage = 1;
    public float weaponRange = 50f;
    public Transform firePoint;

    //public ParticleSystem muzzleParticle;
    private AudioSource gunFireSource;

    private float timer;
    private LineRenderer fireLine;
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.02f);


    void Start()
    {
        fpsCam = Camera.main;
        fireLine = GetComponent<LineRenderer>();
        gunFireSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                timer = 0f;
                FireGun();
                
            }
        }
    }

    // FireGun
    private void FireGun()
    {
        // Play these things on Fire
        //muzzleParticle.Play();
        gunFireSource.Play();

        StartCoroutine(ShotEffect());

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit rayHit;

        fireLine.SetPosition(0, firePoint.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out rayHit, weaponRange))
        {
            fireLine.SetPosition(1, rayHit.point);

            var health = rayHit.collider.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
        else
        {
            fireLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
        }
    }


    private IEnumerator ShotEffect()
    {
        fireLine.enabled = true;
        yield return shotDuration;
        fireLine.enabled = false;
    }
}
