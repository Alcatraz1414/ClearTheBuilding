using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private GameObject ammo;   
    [SerializeField] private bool isPlayer;

    private int ammoCount = 0;
    [SerializeField]private Transform fireTransform;
    private float fireRate = 2f;
    private int maxAmmoCount = 10;

    private float currentFireRate = 0f;

    private AudioClip clipToPlay;
    private AudioSource audioSource;

    private GameManager gameManager;


    public int GetAmmo
    {
        get { 
            return ammoCount;
        } 
        set {
            ammoCount=value;
            if (ammoCount > maxAmmoCount){
                ammoCount = maxAmmoCount; 
            } 
        }

    }

    public float GetCurrentFireRate
    {
        get
        {
            return currentFireRate ;
        }
        set
        {
            currentFireRate =value;
        }
    }

    public int GetClipSize
    {
        get
        {
            return maxAmmoCount;
        }
        set
        {
            maxAmmoCount=value;
        }
    }

    public float GetFireRate
    {
        get { 
            return fireRate; 
        }
        set
        {
            fireRate=value;
        }
    }

    public Transform GetFireTransform
    {
        get
        {
            return fireTransform;
        }
        set
        {
            fireTransform = value;
        }
    }

    public AudioClip GetClipToPlay
    {
        get { return clipToPlay; }
        set { clipToPlay = value; }
    }

    private void Awake()
    {
      audioSource = GetComponent<AudioSource>();
      gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentFireRate > 0)  // ateþ edince currentfirerate fire metdunda arttýrdýðýmýz için zamanla 0 a çekiyor ve yine atýþ hakkýmýz oluyor
        {
            currentFireRate-=Time.deltaTime;
        }


        PlayerInput();

        
    }

    public void PlayerInput()
    {
        if (isPlayer && !gameManager.GetLevelFinish)  // isPlayer true ise ve oyun bitmediyse mouse sol týk ile vur
        {
            if (Input.GetMouseButtonDown(0))  // 0 sol týk, 1 sað týk, 2 orta týk
            {
                if (currentFireRate <= 0 && ammoCount > 0)   // mermi atýþ hýzý ve sýnýrlandýrma 
                {
                    Fire();

                }

            }

            switch (Input.inputString)
            {
                case "1":
                    weapons[1].gameObject.GetComponent<Weapon>().GetCurrentWeaponCount = ammoCount;
                    // bunun sebebi silah deðiþince mermi sayýsý ayný kalsýn diye

                    weapons[0].SetActive(true);
                    weapons[1].SetActive(false);
                    break;
                case "2":
                    weapons[0].gameObject.GetComponent<Weapon>().GetCurrentWeaponCount = ammoCount;
                    // bunun sebebi silah deðiþince mermi sayýsý ayný kalsýn diye

                    weapons[0].SetActive(false);
                    weapons[1].SetActive(true);
                    break;
               
            }

        }
    }

    public void Fire()
    {
        float difference=180f-transform.eulerAngles.y;
        float targetRotation = -90f;

        if (difference >= 90f)       // baktýðýn yöne göre ya saða ya da sola ateþ etme 
        {
            targetRotation = -90f;
        }

        else if (difference<90f)
        {
            targetRotation = 90f;
        }
        ammoCount--;
        currentFireRate = fireRate;  // mermi atýþ hýzý için
        audioSource.PlayOneShot(clipToPlay);

        GameObject bulletClone=Instantiate(ammo,fireTransform.position, Quaternion.Euler(0f,0f,targetRotation));   //oluþturuðpumuz mermiyi bulletClone adlý gameobject e atýyoruz
        bulletClone.GetComponent<Bullet>().owner = gameObject;   // burda ise bu objeynin owner deðerini bu scriptin baðlý olduðpu objeye veriyoruz

        // bunlarýn sebebi kim ateþ ettiyse o merminin sahibinin o oldupunu göstermemiz için yani owner ateþ eden kiþi oluyor.  Bu kodun devamý target scriptinde.
    }

}
