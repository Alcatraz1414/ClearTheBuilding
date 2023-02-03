using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 10;
    public GameObject owner;


    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed *Time.deltaTime;  // transfor.up dememizin sebebi mermi LOCAL de ysi x gibi gözüküyor.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Target>() == false)   // deðen objenin içinde target özelliði( burada script ) yoksa mermiyi yok et.
        {
            Destroy(gameObject);
            
        }
    }

}
