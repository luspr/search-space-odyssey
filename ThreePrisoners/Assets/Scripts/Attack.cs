using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [SerializeField]
    private float rate = 1;

    [SerializeField]
    private int damage = 1;

    [SerializeField]
    private ParticleSystem[] shootyParticles;
    [SerializeField]
    private ParticleSystem hitParticlesPrefab;
    [SerializeField]
    private ParticleSystem bloodParticlesPrefab;
    [SerializeField]
    private AudioClip fireAudioClip;


    private AudioSource fireAudioSource;
    private float attackTimer;


    void Start()
    {
        attackTimer = rate;
        fireAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= rate)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                attackTimer = 0f;
                Fire();
            }
        }
    }

    private void Fire()
    {
        for (int i = 0; i < shootyParticles.Length; i++)
        {
            shootyParticles[i].Play();
        }
        fireAudioSource.PlayOneShot(fireAudioClip);
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 5, Color.red, 5f);
        if (Physics.Raycast(ray, out hit, 100))
        {
            // TODO hit something
            Debug.Log("Hit: " + hit.transform.name);
            Health targetHealth = hit.transform.GetComponent<Health>();
            if (targetHealth != null)
            {
                ParticleSystem bloodParticles = Instantiate(bloodParticlesPrefab, hit.point, Quaternion.identity);
                bloodParticlesPrefab.Play();
                targetHealth.TakeDamage(damage);
            }
            else
            {
                ParticleSystem particles = Instantiate(hitParticlesPrefab, hit.point, Quaternion.identity);
                particles.Play();
            }


        }
    }
}
