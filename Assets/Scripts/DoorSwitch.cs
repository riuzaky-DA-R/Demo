using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public GameObject Door;
    Vector3 startPos;
    Vector3 Vertical = new Vector3(0, 1f, 0).normalized;
    float speed = 1.5f;
    bool Openning = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = Door.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoor(Openning);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
             Openning= true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Openning = false;
        }
    }
    public void OpenDoor(bool closed)
    {
        if (closed)
        {
            if (Door.transform.position.y < 8f)
            {
                Door.transform.position += Vertical * speed * Time.deltaTime;
            }
            else if(Door.transform.position.y > 8f)
            {
                Door.transform.position = Door.transform.position;
            }
        }
        else if (closed == false && Door.transform.position.y>2.4)
        {
            //speed = 1.5f;
            Door.transform.position -= Vertical * speed * Time.deltaTime;
        }
        else if (closed ==false && Door.transform.position.y <= 2.3f)
        {
            Door.transform.position = startPos;
        }
    }
}

