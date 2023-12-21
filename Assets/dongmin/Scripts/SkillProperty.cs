using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProperty : MonoBehaviour
{
    [SerializeField]
    private Element skillElement;

    [SerializeField]
    private float skillDamage;


    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Enemy")
        {
            object[] skillprop = {skillElement, skillDamage};
            other.SendMessage("Damaged", skillprop);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
