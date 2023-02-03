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

        if (currentFireRate > 0)  // ate� edince currentfirerate fire metdunda artt�rd���m�z i�in zamanla 0 a �ekiyor ve yine at�� hakk�m�z oluyor
        {
            currentFireRate-=Time.deltaTime;
        }


        PlayerInput();

        
    }

    public void PlayerInput()
    {
        if (isPlayer && !gameManager.GetLevelFinish)  // isPlayer true ise ve oyun bitmediyse mouse sol t�k ile vur
        {
            if (Input.GetMouseButtonDown(0))  // 0 sol t�k, 1 sa� t�k, 2 orta t�k
            {
                if (currentFireRate <= 0 && ammoCount > 0)   // mermi at�� h�z� ve s�n�rland�rma 
                {
                    Fire();

                }

            }

            switch (Input.inputString)
            {
                case "1":
                    weapons[1].gameObject.GetComponent<Weapon>().GetCurrentWeaponCount = ammoCount;
                    // bunun sebebi silah de�i�ince mermi say�s� ayn� kals�n diye

                    weapons[0].SetActive(true);
                    weapons[1].SetActive(false);
                    break;
                case "2":
                    weapons[0].gameObject.GetComponent<Weapon>().GetCurrentWeaponCount = ammoCount;
                    // bunun sebebi silah de�i�ince mermi say�s� ayn� kals�n diye

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

        if (difference >= 90f)       // bakt���n y�ne g�re ya sa�a ya da sola ate� etme 
        {
            targetRotation = -90f;
        }

        else if (difference<90f)
        {
            targetRotation = 90f;
        }
        ammoCount--;
        currentFireRate = fireRate;  // mermi at�� h�z� i�in
        audioSource.PlayOneShot(clipToPlay);

        GameObject bulletClone=Instantiate(ammo,fireTransform.position, Quaternion.Euler(0f,0f,targetRotation));   //olu�turu�pumuz mermiyi bulletClone adl� gameobject e at�yoruz
        bulletClone.GetComponent<Bullet>().owner = gameObject;   // burda ise bu objeynin owner de�erini bu scriptin ba�l� oldu�pu objeye veriyoruz

        // bunlar�n sebebi kim ate� ettiyse o merminin sahibinin o oldupunu g�stermemiz i�in yani owner ate� eden ki�i oluyor.  Bu kodun devam� target scriptinde.
    }

}
