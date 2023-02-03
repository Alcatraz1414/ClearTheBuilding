using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Health Settings")]
    public bool healthPowerUp=false;
    public int healthAmount = 1;
    [Header("Ammo Settings")]
    public bool ammoPowerUp=false ;
    public int ammoAmount = 5;
    [Header("Transform Settings")]
    [SerializeField] private Vector3 turnVector=Vector3.zero;

    // bu k�s�m objenin b�y�y�p k���lmesi ile ilgili
    [Header("Scale Settings")]
    [SerializeField] private float period = 2f;  
    [SerializeField] Vector3 scaleVector;
    [SerializeField] private float scaleFactor;
    private Vector3 startScale;

    private AudioSource audioSource;
    [SerializeField] private AudioClip clipToPlay;


    private void Awake()
    {
        startScale = transform.localScale;
        audioSource= GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

        // a��k unutmamak i�in ba�lang��ta false yap�yoruz
        if(ammoPowerUp && healthPowerUp)
        {
            ammoPowerUp = false;
            healthPowerUp = false;
        }
        else if (ammoPowerUp)
        {
            healthPowerUp=false;
        }
        else if (healthPowerUp)
        {
            ammoPowerUp=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(turnVector);

        SinusWawe();
    }

    private void SinusWawe()  //            ????                scale art�p-azalma k�sm�
    {
        if(period <= 0f)
        {
            period = 0.1f;
        }

        float cycles =Time.timeSinceLevelLoad/period;  // ne kadar s�rede b�y�yece�i
        const float piX2 = Mathf.PI * 2;
        float sinusWawe= Mathf.Sin(cycles * piX2);// bura -1 ve 1 aras�nda de�er d�nd�r�yor ve scale - olursa obje yok oalca��ndan 65. sat�rdakini yap�yopruz yani 0 ile 1 oluyor

        scaleFactor = sinusWawe / 2 + 0.5f;
        Vector3 offset = scaleFactor * scaleVector;  

        transform.localScale=startScale+ offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {

            AudioSource.PlayClipAtPoint(clipToPlay,transform.position);

            if (healthPowerUp)
            {
                other.gameObject.GetComponent<Target>().GetHealth += healthAmount;
            }
            else if (ammoPowerUp)
            {
                other.gameObject.GetComponent<Attack>().GetAmmo+=ammoAmount;
            }
            Destroy(gameObject);
        }
        


    }

}
