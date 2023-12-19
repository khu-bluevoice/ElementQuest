using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Spell_Card : MonoBehaviour
{
    [HideInInspector]
    public bool isFlipped = false;

    bool isFlipping = false;
    float flippingTime = 0.3f;

    [SerializeField]
    public Spell spell;

    [SerializeField]
    SpellResourceLoader loader;

    [SerializeField]
    SpriteRenderer ElementIcon;

    [SerializeField]
    SpriteRenderer CardBack;

    [SerializeField]
    SpriteRenderer CardFront;

    [SerializeField]
    SpriteRenderer CardFrame;

    [SerializeField]
    SpriteRenderer CardHand;

    [SerializeField]
    SpriteRenderer CardText;

    [SerializeField]
    SpriteRenderer CardRibbon;

    [SerializeField]
    GameObject DescriptionText;

    // Start is called before the first frame update
    void Start()
    {
        if (spell)
        {
            // set spell info
            DescriptionText.GetComponent<TMPro.TextMeshPro>().text = spell.description;
            // load sprite resources
            SpellResourceItem item = loader.getResourceOfElement(spell.element);
            // set sprite resources
            ElementIcon.sprite = item.cardElementSprite;
            CardBack.sprite = item.cardBackSprite;
            CardFront.sprite = item.cardFrontSprite;
            CardFrame.sprite = item.cardFrameSprite;
            CardHand.sprite = item.cardHandPoseSprite;
            CardText.sprite = item.cardTextContainerSprite;
            CardRibbon.sprite = item.cardRibbonSprite;
        }
        if(this.isFlipped)
        {
            CardText.transform.Rotate(0, 180, 0);
        }
        // reorder sprites
        UpdateSpriteOrder(this.isFlipped);
    }

    float time = 0;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 3)
        {
            Flip();
            time = 0;
        }
    }

    public void Flip()
    {
        if (!isFlipping)
        {
            StartCoroutine(PlayFlippingEffect());
        }
    }

    float angleBeforeFlip = 0;
    IEnumerator PlayFlippingEffect()
    {
        this.isFlipping = true;
        this.isFlipped = !this.isFlipped;

        float t = 0;

        float angle = 0;
        while (t <= flippingTime)
        {
            angle = t / flippingTime * 180 + angleBeforeFlip;
            //print(transform.eulerAngles);
            //print("local" + transform.localRotation);
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);
            //transform.localRotation = Quaternion.LookRotation(transform.up).;
            transform.Rotate(0, Time.deltaTime / flippingTime * 180, 0);

            t += Time.deltaTime;

            if (Mathf.Floor(angle) % 180 > 90)
            {
                UpdateSpriteOrder(this.isFlipped);
            }

            yield return null;
        }
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, this.isFlipped ? 180 : 0, transform.localEulerAngles.z);

        this.angleBeforeFlip = this.isFlipped ? 180 : 0;
        this.isFlipping = false;

        yield return 0;
    }

    void UpdateSpriteOrder(bool isFlipped)
    {
        if (isFlipped)
        {
            ElementIcon.sortingOrder = 2;
            CardBack.sortingOrder = 1;
            foreach (SpriteRenderer item in CardFront.GetComponentsInChildren<SpriteRenderer>())
            {
                item.sortingOrder = -1;
            }
            DescriptionText.GetComponent<TMPro.TextMeshPro>().sortingOrder = -1;
        }
        else
        {
            CardRibbon.sortingOrder = 4;
            CardFrame.sortingOrder = 3;
            CardHand.sortingOrder = 2;
            CardText.sortingOrder = 2;
            DescriptionText.GetComponent<TMPro.TextMeshPro>().sortingOrder = 3;
            CardFront.sortingOrder = 1;
            foreach (SpriteRenderer item in CardBack.GetComponentsInChildren<SpriteRenderer>())
            {
                item.sortingOrder = -1;
            }
        }
    }
}
