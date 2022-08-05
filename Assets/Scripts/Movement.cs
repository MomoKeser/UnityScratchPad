    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Movement : MonoBehaviour
    {
        public float stamina;
        public float maxStamina;

        public float runningStaminaThreshold;
        public float walkingSpeed; //1000
        public float runningSpeed; //3000
        private float currentSpeed;
        private float staminaDecrement = 3f;
        private float staminaIncrement = 2f;

        public UnityEngine.UI.Slider slider;
        
        void Start()
        {
            stamina = maxStamina;
        }

        void Update()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 inputVector = new Vector3(
                Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime, //x = left and right
                0f, //y = up and down
                Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime); // z = backward and forward
            rb.AddForce(inputVector);

            //Jump
            if(Input.GetKey(KeyCode.Space))
            {
                Debug.DrawRay(transform.position, Vector3.down * 1, Color.blue, 2f);
                if(Physics.Raycast(transform.position, Vector3.down, 1.1f))
                {
                    rb.velocity = new Vector3(
                        rb.velocity.x,
                        10f,
                        rb.velocity.z);
                }
            }
            
            //Run Input
            if(Input.GetKey(KeyCode.LeftShift))
            {
                //TODO: Decrease stamina
              currentSpeed = runningSpeed;
              stamina -= staminaDecrement * Time.deltaTime;
              if(stamina < 0f)
              {
                stamina = 0f;
              }
              
              if(stamina == 0f)
              {
                currentSpeed = walkingSpeed;
              }
            }
            else
            {
                currentSpeed = walkingSpeed;
                stamina += staminaIncrement * Time.deltaTime;
              
              if(stamina > maxStamina)
              {
                stamina = maxStamina;
              }
            }

        slider.value = stamina / maxStamina;
            
        }
    }
