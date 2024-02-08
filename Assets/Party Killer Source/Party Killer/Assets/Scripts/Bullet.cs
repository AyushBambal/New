using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks
{

	public GameObject explosionPrefab;

	int bounce = 2;

	float lastBounceTime = 0f;

	PhotonView shooterPhotonView;

	public void SetShooter(PhotonView shooter)
	{
		shooterPhotonView = shooter;
		
	}

	private void OnCollisionEnter(Collision col)
	{
		if (col.collider.CompareTag("Player"))
		{
			PhotonView playerPV = col.collider.GetComponent<PhotonView>();
			if (playerPV != null && playerPV != shooterPhotonView)
			{
				float damage = 10.0f;
				playerPV.RPC("TakeDamage", RpcTarget.All, damage);
			}
		}



		// Continue with your damage logic...

		else if (col.collider.CompareTag("Bullet"))
		{
			Explode();
		}
		else
		{
			if (Time.time >= lastBounceTime + .05f)
			{
				if (bounce == 0)
				{
					Explode();
				}
				else
				{
					bounce--;
					lastBounceTime = Time.time;

					AudioManager.instance.Play("Bounce");
				}
			}
		}

		}

		void Explode()
		{
			AudioManager.instance.Play("Explode");

			GameObject explode = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			Destroy(explode, 10f);
			Destroy(gameObject);

			CameraShaker.Instance.ShakeOnce(2f, 2f, .05f, .35f);
		}

	}

