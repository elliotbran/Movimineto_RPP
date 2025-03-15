using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer cardImageRenderer;
    [SerializeField] TextMeshPro cardTextRenderer;

    WaveManag waveManag;

    private CardSO cardInfo;

    private void Start()
    {
        waveManag = FindObjectOfType<WaveManag>();
    }
    public void Setup(CardSO card)
    {
        cardInfo = card;
        cardImageRenderer.sprite = card.cardImage;
        cardTextRenderer.text = card.cardText;
    }

    private void OnMouseDown()
    {
        waveManag.startnwave();
        Debug.Log("You clicked a card");
        CardManager.Instance.SelectCard(cardInfo);
    }
}
