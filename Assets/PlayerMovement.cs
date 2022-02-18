using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;
    public Rigidbody rigidBody;
    float mainSpeed = 1.0f; //regular speed
    float shiftAdd = 2.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 4.0f; //Maximum speed when holdin gshift
    float camSens = 0.25f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;
    private bool isFlying = false;
    private bool canWalkForward = true;
    private bool canWalkBackward = true;
    private float rayDistance = 1000.0f;
    private bool collidedWithRealObject = false; //Determines if object should not be passable
    private string interactWithAnimal = "";

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();

        //Make hen names
        //GlobalVar.henNames = new List<string>();
        //GlobalVar.henNames.Add("Charlie");
        //GlobalVar.henNames.Add("Helen");
        //GlobalVar.henNames.Add("Linda");
        //GlobalVar.henNames.Add("Franny");
    }
    void FixedUpdate()
    {
        


        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 bkwd = transform.TransformDirection(Vector3.back);

        //Tells raycast to not walk forward if there is an object in the way, stops all animations
        if (Physics.Raycast(transform.position, fwd, 0.01f))
        {
            print("Object infront of me!");
            //Only stop movement if gameobject is a true object and not just an area collider
            if (collidedWithRealObject == true)
            {
                canWalkForward = false;

            }
            //Debug.Log(collision.collider.gameObject.name);
            
            
        }
        else
        {
            canWalkForward = true;
        }
        if (Physics.Raycast(transform.position, bkwd, 0.1f))
        {
            print("Object behind me!");
            //Only stop movement if gameobject is a true object and not just an area collider
            if (collidedWithRealObject == true)
            {
                canWalkBackward = false;

            }
            //Debug.Log(collision.collider.gameObject.name);


        }
        else
        {
            canWalkBackward = true;
        }

    }
    void Update()
    {
        GlobalVar.talkToFriendly = interactWithAnimal;
        Debug.Log(interactWithAnimal);
        //Debug.Log(GlobalVar.henNames[0]);

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);



        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;
        //Mouse  camera angle done.  

        //Keyboard commands
       
        float f = 0.0f;
        Vector3 p = GetBaseInput();
            if (p.sqrMagnitude > 0)
            { // only move while a direction key is pressed
                p = p * Time.deltaTime;
                Vector3 newPosition = GetComponent<Rigidbody>().position;

                if (Input.GetKey(KeyCode.LeftShift)&& Input.GetKey(KeyCode.W))
                {

                    totalRun += Time.deltaTime;
                    p = p * totalRun * shiftAdd;

                    p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                    p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                    p.z = Mathf.Clamp(p.z, -maxShift, maxShift);

                    
                    if (Input.GetKey(KeyCode.Space) && GlobalVar.flyingPowerCooldown == false)
                    { //If player wants to move on X and Z axis only
                   
                            animator.Play("Hen_Flying");
                            transform.Translate(p);
                            newPosition.x = GetComponent<Rigidbody>().position.x;
                            newPosition.z = GetComponent<Rigidbody>().position.z;
                            GetComponent<Rigidbody>().position = newPosition;
                            maxShift = 10.0f;
                            shiftAdd = 3.0f;
                            GlobalVar.flyingPower -= 0.001f;
                            if (GlobalVar.flyingPower <= 0.0f)
                            {
                                GlobalVar.flyingPowerCooldown = true;
                            }
                      
                     
                    }
                    else
                    {
                        transform.Translate(p);
                        animator.Play("Hen_Run");
                        maxShift = 4.0f;
                        shiftAdd = 2.0f;
                    }

                }
                else
                {
                    if (Input.GetKey(KeyCode.S))
                    {
                        animator.Play("Hen_Walk_Reverse");
                        transform.Translate(p);

                    }
                    else
                    {
                        animator.Play("Hen_Walk");
                        transform.Translate(p);
                    
                    }


                }
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                p = p * mainSpeed;

            }
        
    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();

        if (canWalkForward)
        {
            if (Input.GetKey(KeyCode.W))
            {

                p_Velocity += new Vector3(0, 0, 1);
            }
        }
        if (canWalkBackward) { 
            if (Input.GetKey(KeyCode.S))
            {

                p_Velocity += new Vector3(0, 0, -1);
            }
        }
        //if (Input.GetKey(KeyCode.A))
        //{
        //    p_Velocity += new Vector3(-1, 0, 0);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    p_Velocity += new Vector3(1, 0, 0);
        //}
        return p_Velocity;
    }

    void OnTriggerEnter(Collider collision)
    {

        //Tells raycast to not walk forward if there is an object in the way, stops all animations
        //if (Physics.Raycast(transform.position, fwd, 0.1f))
        //{
        //    print("Object infront of me!");
        //        //Debug.Log(collision.collider.gameObject.name);

        //    canWalkForward = false;
        //}
        //else
        //{
        //    canWalkForward = true;
        //}
        //If collision is an Animal
       

        //If collision is an "Area"
        if (collision.name == "AreaHouse")
        {
            GlobalVar.area = "The House";
            collidedWithRealObject = false;
         
        }else if (collision.name == "AreaBarn")
        {
            GlobalVar.area = "The Barn";
            collidedWithRealObject = false;
        }
        else if (collision.name == "AreaGarden")
        {
            GlobalVar.area = "The Garden";
            collidedWithRealObject = false;
        }
        else if (collision.name == "AreaCoop")
        {
            GlobalVar.area = "The Coop";
            collidedWithRealObject = false;
        }
        else
        {
            rigidBody.velocity = Vector3.zero;
            
            collidedWithRealObject = true;
            //if (collision.tag == "Animal")
            //{
            //    GlobalVar.talkToFriendly = "Talk to " + collision.name;
            //    Debug.Log(collision.name);
            //}
            //GlobalVar.talkToFriendly = "";

        }

       

      

        //if (collision.tag == "Animal")
        //{
        //    GlobalVar.talkToFriendly = "Talk to " + collision.name;
        //}
        //else
        //{
        //    GlobalVar.talkToFriendly = "";
        //}
        //Display popups
    }
    void OnTriggerStay(Collider collision)
    {
       
       
        if (collision.tag == "HenFriend")
        {
            char henIndex = collision.name[collision.name.Length - 1];
            int i = int.Parse(henIndex.ToString());
            Debug.Log(GlobalVar.henNames[i]);
            string animalString = "(c) Talk to " + GlobalVar.henNames[i];
            
            
            SetTalker(animalString);
            
           
            //Debug.Log("ANIMAL!");
        }
        else if(collision.tag == "DogFriend")
        {
            string animalString = "(c) Talk to " + GlobalVar.dogName;


            SetTalker(animalString);
        }
        


    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Triggered!");

    }
    void OnTriggerExit(Collider collision)
    {
        //Debug.Log("LEFT " + collision.name);
        //if (collision.tag == "HenFriend"||collision.tag =="DogFriend")
       // {
            SetTalker("");
       // }
    //
    }

    void SetTalker(string animalString)
    {
        interactWithAnimal = animalString;
    }


}
