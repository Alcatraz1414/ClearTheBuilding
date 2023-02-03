using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float speed = 5f;

    [SerializeField] private float shootRange = 10f;
    [SerializeField] private LayerMask shootLayer;
    [SerializeField] private Transform aimTransform;
    [SerializeField] private float reloadTime = 5f;

    private bool isReloaded=false;  //d��man�n mermisi bitince doldurmas� i�in ge�en s�re

    [SerializeField] private bool canMoveRight = false;

    private Attack attack;

   
    private void Awake()
    {
        attack = GetComponent<Attack>();

        aimTransform = attack.GetFireTransform;
    }

    // Update is called once per frame
    void Update()
    {

        
        EnemyAttack();
        CheckCanMoveRight();
        MoveToward();
        Aim();
    }

    private void Reload()  // d��man i�in mermi de�i�tirme
    {
        attack.GetAmmo = attack.GetClipSize; // propertyler ile yapt�k (get,set olanlar)  bu metot �al���nca o anki mermi max mermiye e�itlenecek
        isReloaded= false;
    }

    private void EnemyAttack()
    {

        if (attack.GetAmmo <= 0  && isReloaded== false)
        {
            Invoke(nameof(Reload), reloadTime);  // a�a��daki ile ayn� �ey
            //Invoke("Reload", reloadTime);
            isReloaded=true;
        }

            if (attack.GetCurrentFireRate <= 0f && attack.GetAmmo > 0 && Aim())
            {
                attack.Fire();
            }
    }

    private void MoveToward()
    {

        if(Aim() && attack.GetAmmo > 0)  // e�er ate� edebilecek bir �ey varsa ve mermi 0 dan fazla ise a�a��dakileri yapma �zetle: vuraca� zaman duruyor.
        {
            return;
        }

        if (canMoveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[1].position.x,transform.position.y, movePoints[1].position.z), speed * Time.deltaTime);
            // bu k�s�mda newVector3 l� yapmam�n sebebi bu 2 nokta aras�nda giderken y sabit kals�n diye

            lookAtTheTarget(movePoints[1].position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[0].position.x, transform.position.y, movePoints[1].position.z), speed * Time.deltaTime);
            // bu k�s�mda newVector3 l� yapmam�n sebebi bu 2 nokta aras�nda giderken y sabit kals�n diye

            lookAtTheTarget(movePoints[0].position);
        }
    }

    private void CheckCanMoveRight()
    {
        if (Vector3.Distance(transform.position, movePoints[0].position) <= 1f)
        {
            canMoveRight = true;
            print("sa�a git");
        }
        else if (Vector3.Distance(transform.position, movePoints[1].position) <= 0.1f)
        {
            canMoveRight = false;
            print("sola git");
        }
    }

    private void lookAtTheTarget(Vector3 newTarget)
    {
        Vector3 newLookPosition = new Vector3(newTarget.x,transform.position.y,newTarget.z); // buras� y si sabit kals�n diye yapt�k

        Quaternion targetRotation = Quaternion.LookRotation(newLookPosition- transform.position);  // d�n�lecek y�n� bulduk
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed*Time.deltaTime);
    }

    private bool Aim()   // d��man alan�na giriyormuyuz onu true-false d�ndr�yor
    {

        if (aimTransform == null)  // bu if k�sm�n� ve i�indekini oyun restart edince hata verdi�i i�in yapt�k.
        {
            aimTransform = attack.GetFireTransform; 
        }
       
        bool hit = Physics.Raycast(aimTransform.position, transform.forward, shootRange, shootLayer);
        Debug.DrawRay(aimTransform.position, transform.forward*shootRange, Color.blue);
        return hit;
    }

    

}
