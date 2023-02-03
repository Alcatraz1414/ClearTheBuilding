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

    private bool isReloaded=false;  //düþmanýn mermisi bitince doldurmasý için geçen süre

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

    private void Reload()  // düþman için mermi deðiþtirme
    {
        attack.GetAmmo = attack.GetClipSize; // propertyler ile yaptýk (get,set olanlar)  bu metot çalýþýnca o anki mermi max mermiye eþitlenecek
        isReloaded= false;
    }

    private void EnemyAttack()
    {

        if (attack.GetAmmo <= 0  && isReloaded== false)
        {
            Invoke(nameof(Reload), reloadTime);  // aþaðýdaki ile ayný þey
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

        if(Aim() && attack.GetAmmo > 0)  // eðer ateþ edebilecek bir þey varsa ve mermi 0 dan fazla ise aþaðýdakileri yapma özetle: vuracað zaman duruyor.
        {
            return;
        }

        if (canMoveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[1].position.x,transform.position.y, movePoints[1].position.z), speed * Time.deltaTime);
            // bu kýsýmda newVector3 lü yapmamýn sebebi bu 2 nokta arasýnda giderken y sabit kalsýn diye

            lookAtTheTarget(movePoints[1].position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[0].position.x, transform.position.y, movePoints[1].position.z), speed * Time.deltaTime);
            // bu kýsýmda newVector3 lü yapmamýn sebebi bu 2 nokta arasýnda giderken y sabit kalsýn diye

            lookAtTheTarget(movePoints[0].position);
        }
    }

    private void CheckCanMoveRight()
    {
        if (Vector3.Distance(transform.position, movePoints[0].position) <= 1f)
        {
            canMoveRight = true;
            print("saða git");
        }
        else if (Vector3.Distance(transform.position, movePoints[1].position) <= 0.1f)
        {
            canMoveRight = false;
            print("sola git");
        }
    }

    private void lookAtTheTarget(Vector3 newTarget)
    {
        Vector3 newLookPosition = new Vector3(newTarget.x,transform.position.y,newTarget.z); // burasý y si sabit kalsýn diye yaptýk

        Quaternion targetRotation = Quaternion.LookRotation(newLookPosition- transform.position);  // dönülecek yönü bulduk
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed*Time.deltaTime);
    }

    private bool Aim()   // düþman alanýna giriyormuyuz onu true-false döndrüyor
    {

        if (aimTransform == null)  // bu if kýsmýný ve içindekini oyun restart edince hata verdiði için yaptýk.
        {
            aimTransform = attack.GetFireTransform; 
        }
       
        bool hit = Physics.Raycast(aimTransform.position, transform.forward, shootRange, shootLayer);
        Debug.DrawRay(aimTransform.position, transform.forward*shootRange, Color.blue);
        return hit;
    }

    

}
