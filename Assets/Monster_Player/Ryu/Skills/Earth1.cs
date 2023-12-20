using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth1 : Skill2
{
    // Start is called before the first frame update
    void Start()
    {
        element = "Earth";
        damage = 10;
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 200, 0));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 5 * Time.deltaTime;
    }

}
