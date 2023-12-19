using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSelector : MonoBehaviour
{
    [SerializeField]
    public Spell[] spells;

    public GameObject spellCardPrefab;

    float width = 18;
    float height = 30;

    // Start is called before the first frame update
    void Start()
    {
        float startX = spells.Length * width / -2 + width / 2;
        //float startX = spells.Length * width;
        Vector3 startPoint = new Vector3(0, -100, 50);
        int centerIndex = spells.Length / 2;
        for (int i = 0; i < spells.Length; i++)
        {
            Vector3 position = new Vector3(startX + i * width, 0, 50);
            //GameObject a = Instantiate(spellCardPrefab, position, Quaternion.identity);
            GameObject a = Instantiate(spellCardPrefab, position, Quaternion.LookRotation(startPoint - position));
            if (i < centerIndex)
            {
                a.transform.Rotate(new Vector3(0, 90, -90));
                a.transform.Rotate(new Vector3(0, 180, 0));
            }
            else a.transform.Rotate(new Vector3(0, 90, -90));
            a.GetComponent<Spell_Card>().spell = spells[i];
            a.GetComponent<Spell_Card>().isFlipped = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
