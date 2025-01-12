using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damage = 20;
    public float interval = 0.1f;
    public float range = 100f;

    private float timer;
    private Ray shootRay;
    private RaycastHit rayHit;
    private int shootableMask;
    private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private AudioSource gunAudio;
    private Light gunLight;
    private float effectsDisplayTime = 0.2f;



    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(Input.GetButton("Fire1") && timer >= interval)
        {
            Fire();
        }

        if(timer >= interval * effectsDisplayTime)
        {
            DisableEffects();
        }

    }      

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
    private void Fire()
    {
        timer = 0f;

        gunAudio.Play();
        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out rayHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = rayHit.collider.GetComponent<EnemyHealth>();            
            enemyHealth?.TakeDamage(damage, rayHit.point);
            gunLine.SetPosition(1, rayHit.point);    
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
