using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }
    [PunRPC]
    public void Shoot()
    {
        if (!PV.IsMine)
        {
            Debug.LogWarning("Trying to shoot, but not the local player's instance.");
            return;
        }

        // Create a bullet and pass the shooter's PhotonView
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = projectile.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            // Set the shooter PhotonView and ViewID directly
            bulletScript.SetShooter(PV);
        }
        else
        {
            Debug.LogError("Bullet script not found on the projectile!");
        }

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(firePoint.forward * projectileSpeed, ForceMode.VelocityChange);
        }
        else
        {
            Debug.LogError("Projectile is missing Rigidbody component!");
        }
    }
}