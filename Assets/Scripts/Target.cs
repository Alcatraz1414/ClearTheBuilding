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
    public int GetHealth          // properties deniliyor ve inspectorda gözükmüyor.
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

    public int GetMaxHealth          // properties deniliyor ve inspectorda gözükmüyor.
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
         
         genel mantýk:   bullet scriptinde owner diye bir GameObject var. attack scriptinde mermiyi oluþturduktan sonra bulletclone adlý bir objeye atýyoruz ve oluþturduðumuz bu merminin
         bullet scriptindeki owner objesine atýyoruz bu sayede merminin sahibi belli oluyor.  aþaðýda yaptýðýmýz ise deðen objenin içinde bullet adlý bir özellik(script) var mý diye bakýyoruz
        varsa bu scriptin takýlý oldupu objenin owneri deðilse(kendi mermisinden vurulmasýn diye)  aþaðýdaki can gitme gibi iþlemleri yap diyoruz. 
        
         */



        Bullet bullet = other.gameObject.GetComponent<Bullet>();

        if (bullet)  // deðen þeyin içinde bullet adýnda özellik varsa(burada script oluyor) yani null deðilse
        {
            if (bullet.owner!=gameObject)  // buranýn amacý:  bulletin sahibi bu scriptin baðlý olduðu obje deðilse aþaðýdakileri uygula
            {
                currentHealth--;
                //audioSource.PlayOneShot(clipToPlay);  böyle yaparsam obje yok olduðu andaki ses çýkmaz örnek 3 caný varken 3 kere ses öýkarmasý gerekirken 2 kere çýkarýr çünkü sonuncusunda ses çýkacakken obje yok olmuþ olur.
                AudioSource.PlayClipAtPoint(clipToPlay, transform.position);  // bu ise direkt position verilen yerde o sesi oynatýr.

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
