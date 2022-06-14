using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawController : MonoBehaviour
{
    public int angle = 0;
    bool goRight = false;

    private void Update()
    {
        if (goRight)
        {
            this.transform.Rotate(new Vector3(0, angle, 0)*Time.deltaTime);

            if (this.transform.rotation.x<-0.65f)
            {
                goRight=false;
            }
        }
        else
        {
            this.transform.Rotate(new Vector3(0, -angle, 0)*Time.deltaTime);

            if (this.transform.rotation.x>-0.35f) 
            {
                goRight=true;
            }
        }
    }
}
