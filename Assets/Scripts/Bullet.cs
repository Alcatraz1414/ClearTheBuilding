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
        transform.position += transform.up * speed *Time.deltaTime;  // transfor.up dememizin sebebi mermi LOCAL de ysi x gibi g�z�k�yor.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Target>() == false)   // de�en objenin i�inde target �zelli�i( burada script ) yoksa mermiyi yok et.
        {
            Destroy(gameObject);
            
        }
    }

}
