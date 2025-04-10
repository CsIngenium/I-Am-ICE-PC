using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mov_ojos : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    Camera maincamera;

    public pymv Pymv;

    public Transform objetivo;

    public Rigidbody2D rb;

    public CircleCollider2D col;

    public bool reiniciar = false;


    private void Start()
    {
    	if(target == null)
    		target = transform;

    	if(maincamera == null)
    		maincamera = Camera.main;
    }

    void Update()
    {
        if(Pymv.transform.localScale.y < 0.12)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            col.isTrigger = false;
        }
        else
    	{
            if(Pymv.rotado == false)
                {
                    transform.position = new Vector3(objetivo.position.x + 0.03f, objetivo.position.y - 0.05f, objetivo.position.z);
                }
                else
                {
                    transform.position = new Vector3(objetivo.position.x - 0.03f, objetivo.position.y - 0.05f, objetivo.position.z);
                }
        
                if(Pymv.estatico == true)
                {
                    Vector3 mouseWorldPosition = maincamera.ScreenToWorldPoint(Input.mousePosition);
                    mouseWorldPosition.z = 0;
        
                    Vector3 lookdir = mouseWorldPosition - target.position;
                    target.right = lookdir;
                }
                else
                {
                    if(Pymv.rotado == false)
                    {
                        Quaternion rot=new Quaternion(0,0,0,0);
                        transform.rotation=rot;
                    }
                    else
                    {
                        Quaternion rot=new Quaternion(0,0,5,0);
                        transform.rotation=rot;
                    }
                }
            }

        //print(Pymv.transform.localScale.y);
    }
}
