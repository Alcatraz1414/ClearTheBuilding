using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    Rigidbody rigibody;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 13f;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private Transform[] rayStartPoint;

    private GameManager gameManager;


    private void Awake()
    {
        rigibody = GetComponent<Rigidbody>();     
        gameManager= FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GetLevelFinish) // eðer oyun bitmediyse
        {
            TakeInput();
            // print(onGroundCheck());  // metodun çalýþýp çalýþmadýpýna bakmak için
        }

        onGroundCheck();

    }

    private void TakeInput()
    {

        if (Input.GetKeyDown(KeyCode.Space) && onGroundCheck())
        {
            rigibody.velocity = new Vector3(rigibody.velocity.x,  Mathf.Clamp((jumpPower*100) * Time.deltaTime,12f,13f),   0f);  // böyle yapamýzn sebebi a veya d ile spaceye ayný anda almýyor???        
        }

        if (Input.GetKey("a")) 
        {
            
            rigibody.velocity = new Vector3( Mathf.Clamp((speed*100) * Time.deltaTime,6f,7f),    rigibody.velocity.y,    0f);
            //transform.rotation = Quaternion.Euler(0f,-180f,0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 90f, 0f), turnSpeed*Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.D))  // sadece if yapýnca olmuyor a çalýþmýyor???
        {
            
            rigibody.velocity = new Vector3( Mathf.Clamp( (-speed*100) * Time.deltaTime, -7f, -6f),     rigibody.velocity.y,    0f);
            //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, -90f, 0f), turnSpeed * Time.deltaTime);
        }
        else
        {
            rigibody.velocity = new Vector3(0f, rigibody.velocity.y, 0f);
        }
    }

    private bool onGroundCheck()
    {
        bool hit = false;

        for(int i=0; i < rayStartPoint.Length; i++)
        {
            hit=Physics.Raycast(rayStartPoint[i].position, -rayStartPoint[i].transform.up, 0.25f);
            Debug.DrawRay(rayStartPoint[i].position, -rayStartPoint[i].transform.up * 0.25f, Color.red);
            
        }

        if (hit == true)   //(hit)  yazarsak da true eþitse demek,  (!hit) false ise demek
         {
            return true;
        }
        else
        {
            return false;
        }

    }


}
