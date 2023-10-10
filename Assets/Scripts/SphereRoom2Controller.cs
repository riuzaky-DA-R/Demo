using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRoom2Controller : MonoBehaviour
{
    public float Speed = 1.5f;
    float Signo = -1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PatrolMovement(Signo);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TopeDerecha")
        {
            Signo = -Signo;
            print(Signo);
        }
        else if(collision.gameObject.tag== "TopeIzquierda")
        {
            Signo = -Signo;
            print(Signo);
        }
    }
    public void PatrolMovement(float direction)
    {
        Vector3 Direction = new Vector3(0, 0, direction).normalized;
        transform.position += Direction * Speed * Time.deltaTime;
    }
}
