using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{

    [SerializeField] private int maxHealth=3;
    [SerializeField]private int currentHealth;

    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject deadEffect;

     private AudioSource audioSource;
    [SerializeField] private AudioClip clipToPlay;
    public int GetHealth          // properties deniliyor ve inspectorda g�z�km�yor.
    {
        get { 
            return currentHealth;
        }
        set { 
            currentHealth = value;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
                
            }
        }
    }

    public int GetMaxHealth          // properties deniliyor ve inspectorda g�z�km�yor.
    {
        get
        {
            return maxHealth;
        }
    }


    private void Awake()
    {
        currentHealth = maxHealth;
        audioSource=GetComponent<AudioSource>();

    }


    private void OnTriggerEnter(Collider other)
    {


        /* 
         
         genel mant�k:   bullet scriptinde owner diye bir GameObject var. attack scriptinde mermiyi olu�turduktan sonra bulletclone adl� bir objeye at�yoruz ve olu�turdu�umuz bu merminin
         bullet scriptindeki owner objesine at�yoruz bu sayede merminin sahibi belli oluyor.  a�a��da yapt���m�z ise de�en objenin i�inde bullet adl� bir �zellik(script) var m� diye bak�yoruz
        varsa bu scriptin tak�l� oldupu objenin owneri de�ilse(kendi mermisinden vurulmas�n diye)  a�a��daki can gitme gibi i�lemleri yap diyoruz. 
        
         */



        Bullet bullet = other.gameObject.GetComponent<Bullet>();

        if (bullet)  // de�en �eyin i�inde bullet ad�nda �zellik varsa(burada script oluyor) yani null de�ilse
        {
            if (bullet.owner!=gameObject)  // buran�n amac�:  bulletin sahibi bu scriptin ba�l� oldu�u obje de�ilse a�a��dakileri uygula
            {
                currentHealth--;
                //audioSource.PlayOneShot(clipToPlay);  b�yle yaparsam obje yok oldu�u andaki ses ��kmaz �rnek 3 can� varken 3 kere ses ��karmas� gerekirken 2 kere ��kar�r ��nk� sonuncusunda ses ��kacakken obje yok olmu� olur.
                AudioSource.PlayClipAtPoint(clipToPlay, transform.position);  // bu ise direkt position verilen yerde o sesi oynat�r.

                if(hitEffect!=null && currentHealth>0)
                {
                    Instantiate(hitEffect, transform.position, Quaternion.identity);
                }

                if (currentHealth <= 0)
                {
                    Die();
                    
                }
                Destroy(other.gameObject);
            }
            
        }
        
    }

    private void Die()
    {
        if (hitEffect != null)
        {
            Instantiate(deadEffect, transform.position, Quaternion.identity);
        }      
        Destroy(gameObject);
    }

}
