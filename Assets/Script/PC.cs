using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PC : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed as needed
    PhotonView PV;
    private Vector3 movement;
    PlayerManager playerManager;
    const float maxHealth = 100f;
    public float currentHealth = maxHealth;
    [SerializeField] Image healthbarImage;
    [SerializeField] GameObject ui;
    KillCount killCount;
   



    private void Awake()
    {
        PV = GetComponent<PhotonView>();
      
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();

    }

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(ui);
        }

        

    }

    void Update()
    {

        if (!PV.IsMine)
            return;
        Move();
    }


     void Move()
    {



        // Get user input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Rotate the player to face the movement direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 500f);
        }
      

       
    }
    void FixedUpdate()
    {
        // Move the player in the direction they are facing
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + movement * speed * Time.fixedDeltaTime);
    }

    public void D()
    {
        playerManager.Die();
    }

    public void TakeDamage(float damage)
    {

        PV.RPC(nameof(Rpc_TakeDamage),PV.Owner, damage);// run on shooter computer

    }

    [PunRPC]
    void Rpc_TakeDamage(float damage, PhotonMessageInfo info)
    {


               currentHealth -= damage;
        healthbarImage.fillAmount = currentHealth / maxHealth;
        
          


        if (currentHealth <= 0)
        {
            D();
            PlayerManager.Find(info.Sender).GetKill();
           
        }

       

    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
           
            // Adjust the damage amount based on your game's design
            int damage = 10;
            TakeDamage(damage);
        }
    }

}
