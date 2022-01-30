using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


    public class Player : MonoBehaviour
    {
      // public Rigidbody rigidbody;
      //   public float speed = 30;
        //    public Rigidbody2D rigidbody2d;

        //  private Animator animator;
        //    public Text myName;
        // Start is called before the first frame update
        void Start()
        {

      
        }
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Vector3 position = this.transform.position;
                position.x--;
                this.transform.position = position;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Vector3 position = this.transform.position;
                position.x++;
                this.transform.position = position;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Vector3 position = this.transform.position;
                position.y++;
                this.transform.position = position;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Vector3 position = this.transform.position;
                position.y--;
                this.transform.position = position;
            }
        }

      

    }

