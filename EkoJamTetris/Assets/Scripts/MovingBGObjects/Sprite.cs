using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite : MonoBehaviour
{

    //sprite variables
    bool facingRight = true;
    float lastPosX;
    Vector3 startScale;

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;//scale at the start
        lastPosX = transform.position.x;//last position
    }

    // Update is called once per frame
    void Update()
    {
        //change the direction the sprite is facing
        if (lastPosX < transform.position.x && !facingRight)//moves right but isn't facing right
        {
            facingRight = true;
            flipSprite();
        }
        if (lastPosX > transform.position.x && facingRight)//moves left but isn't facing left
        {
            facingRight = false;
            flipSprite();
        }
        lastPosX = transform.position.x;
    }

    //scale the object so that it's facing the other direction
    public void flipSprite()
    {
        if (!facingRight)
        {
            transform.localScale = new Vector3(-startScale.x, startScale.y, startScale.z);
        }
        else
        {
            transform.localScale = new Vector3(startScale.x, startScale.y, startScale.z);
        }
    }
}
