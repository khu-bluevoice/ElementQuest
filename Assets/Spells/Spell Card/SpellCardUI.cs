using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellCardUI : MonoBehaviour
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
    public Canvas FrontCanvas;

    [SerializeField]
    public Canvas BackCanvas;

    [SerializeField]
    Image ElementIcon;

    [SerializeField]
    Image CardBack;

    [SerializeField]
    Image CardFront;

    [SerializeField]
    Image CardFrame;

    [SerializeField]
    Image CardHand;

    [SerializeField]
    Image CardText;

    [SerializeField]
    Image CardRibbon;

    [SerializeField]
    GameObject DescriptionText;

    [SerializeField]
    SpellIcon spellIcon;

    // Start is called before the first frame update
    void Start()
    {
        if (spell)
        {
            // set spell info
            DescriptionText.GetComponent<TextMeshProUGUI>().text = spell.description;
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
            // update spell icon
            spellIcon.image.sprite = spell.spellIcon;
        }
        if (this.isFlipped)
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
        float totalRotated = 0;
        while (t <= flippingTime)
        {
            angle = t / flippingTime * 180 + angleBeforeFlip;
            //print(transform.eulerAngles);
            //print("local" + transform.localRotation);
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);
            //transform.localRotation = Quaternion.LookRotation(transform.up).;
            float degree = Time.deltaTime / flippingTime * 180;
            totalRotated += degree;
            
            transform.Rotate(0, degree, 0);

            t += Time.deltaTime;

            if (Mathf.Floor(angle) % 180 > 90)
            {
                UpdateSpriteOrder(this.isFlipped);
            }

            yield return null;
        }
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, this.isFlipped ? 180 : 0, transform.localEulerAngles.z);
        if (totalRotated != 180)
        {
            transform.Rotate(0, 180 - totalRotated, 0);
        }

        this.angleBeforeFlip = this.isFlipped ? 180 : 0;
        this.isFlipping = false;

        yield return 0;
    }

    void UpdateSpriteOrder(bool isFlipped)
    {
        if (isFlipped)
        {
            FrontCanvas.sortingOrder = -1;
            BackCanvas.sortingOrder = 1;
            //DescriptionText.GetComponent<TMPro.TextMeshPro>().sortingOrder = -1;
        }
        else
        {
            FrontCanvas.sortingOrder = 1;
            BackCanvas.sortingOrder = -1;
        }
    }
}
