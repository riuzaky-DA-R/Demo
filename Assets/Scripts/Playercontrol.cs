using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrol : MonoBehaviour
{
    public float MovVertical;
    public float MovHorizontal;
    public float Speed = 2f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovHorizontal = Input.GetAxis("Horizontal");
        MovVertical = Input.GetAxis("Vertical");
        Vector3 Direction = new Vector3(MovVertical, 0f, -MovHorizontal);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Speed = 4f;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = 2f;
        }
        //ForwardMove(MovVertical);
        //SidewaysMove(MovHorizontal);
        GeneralMovement(Direction);
        if (Input.GetKey(KeyCode.Q))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.E))
        {
            RotateRight();
        }

    }
    public void GeneralMovement(Vector3 dir)
    {
        Vector3 MovingDirection = transform.TransformDirection(dir);
        rb.velocity = MovingDirection * Speed;
    }
    public void ForwardMove(float frontal)
    {
        Vector3 Direction = new Vector3(frontal, 0, 0).normalized; //normalized so we don´t move faster when holding two keys
        transform.position += Direction * Speed * Time.deltaTime;
    }
    public void SidewaysMove(float lateral)
    {
        Vector3 Direction = new Vector3(0, 0, -lateral).normalized; //normalized so we don´t move faster when holding two keys
        transform.position += Direction * Speed * Time.deltaTime;
    }
    public void RotateRight() 
    {
        transform.Rotate(0, 1f ,0 , Space.Self);
    }
    public void RotateLeft()
    {
        transform.Rotate(0, -1, 0, Space.Self);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="ColorChangeCube")
        {
            MeshRenderer Cubo = collision.gameObject.GetComponent<MeshRenderer>();
            GameObject Room = GameObject.FindGameObjectWithTag("Floor1");
            MeshRenderer RoomFloorColor = Room.GetComponent<MeshRenderer>();
            RoomFloorColor.material.color = Cubo.material.color;
            
        }
        else if(collision.gameObject.tag=="ResetSphere")
        {
            GameObject Reset = GameObject.FindGameObjectWithTag("ResetR2");
            transform.position = Reset.transform.position;
        }
        else if(collision.gameObject.tag== "Proyectil")
        {
            Vector3 dir = collision.contacts[0].point -transform.position;
            dir = dir.normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * 2000f);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LightSwitch")
        {
            GameObject Spot = GameObject.FindGameObjectWithTag("SpotLight");
            Light Spotlight = Spot.GetComponent<Light>();
            Spotlight.color = Color.white;
        }
        else if(other.gameObject.tag=="Cubo")
        {
            GameObject[]Cubos = GameObject.FindGameObjectsWithTag("Cubo");
            for(int i = 0; i<Cubos.Length; i++)
            {
                Cubos[i].gameObject.layer = LayerMask.NameToLayer("Cubo");
            }
            GameObject Cam = GameObject.FindGameObjectWithTag("MainCamera");
            Camera Kodak = Cam.GetComponent<Camera>();
            Kodak.cullingMask = -1;

        }
        else if(other.gameObject.tag=="Cilindro")
        {
            GameObject[] Cilindros = GameObject.FindGameObjectsWithTag("Cilindro");
            for(int i= 0; i< Cilindros.Length; i++)
            {
                Cilindros[i].gameObject.layer = LayerMask.NameToLayer("Cilindro");
            }
            GameObject Cam = GameObject.FindGameObjectWithTag("MainCamera");
            Camera Kodak = Cam.GetComponent<Camera>();
            Kodak.cullingMask = -1;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LightSwitch")
        {
            GameObject Spot = GameObject.FindGameObjectWithTag("SpotLight");
            MeshRenderer CubeColor = other.gameObject.GetComponent<MeshRenderer>();
            Light SpotLight = Spot.GetComponent<Light>();
            SpotLight.color = CubeColor.material.color;

        }
        else if(other.gameObject.tag=="Cubo")
        {
            LayerMask ocultar = 1 << LayerMask.NameToLayer("Cubo");
            other.gameObject.layer = LayerMask.NameToLayer("Visible");
            GameObject Cam = GameObject.FindGameObjectWithTag("MainCamera");
            Camera Kodak = Cam.GetComponent<Camera>();
            Kodak.cullingMask = ~ocultar;
        }
        else if(other.gameObject.tag=="Cilindro")
        {
            other.gameObject.layer = LayerMask.NameToLayer("Visible");//cambia la layer del objeto en el que estamos de cilindro a otra capa que se llama visible
            GameObject Cam = GameObject.FindGameObjectWithTag("MainCamera");
            Camera Kodak = Cam.GetComponent<Camera>();
            //            signo de complemento                  capa a ocultar
            Kodak.cullingMask = ~(1 << LayerMask.NameToLayer("Cilindro"));// crea una culling mask de todas las capas que no sean cilindro, así todos los objetos con la capa cilindro quedan invisibles
        }
    }
}
