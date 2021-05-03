using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Character
{


    // Start is called before the first frame update
    void Start()
    {
        /*
        
        */
    }

    // Update is called once per frame
    public void OnMove(InputValue input)
    {
        Vector2 inputVector = input.Get<Vector2>();
        int dx = 0, dy = 0;
        if (inputVector.x > 0.5f)
        {
            dx = 1;
        }
        else if (inputVector.x < -0.5f)
        {
            dx = -1;
        }
        if (inputVector.y > 0.5f)
        {
            dy = 1;
        }
        else if (inputVector.y < -0.5f)
        {
            dy = -1;
        }
        Move(dx, dy);
    }

}
