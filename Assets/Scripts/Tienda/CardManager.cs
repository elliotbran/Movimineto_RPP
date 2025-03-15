using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject cardSelectionUI;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardPosition1;
    [SerializeField] Transform cardPosition2;
    [SerializeField] Transform cardPosition3;
    [SerializeField] List<CardSO> deck;
    //Currenty randomized cards
    GameObject cardOne, cardTwo, cardThree;

    List<CardSO> alredySelectedCards = new List<CardSO>();

    public Player_Controller playerC;
    public static CardManager Instance;

    void Start()
    {
        RandomizeNewCards();
    }

    private void Awake()
    {
        Instance = this;
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged += HandleGameStateChanged;
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged -= HandleGameStateChanged;
    }
    private void HandleGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.CardSelection)
        {
            RandomizeNewCards();
        }
    }
    void RandomizeNewCards()
    {
        if(cardOne !=null) Destroy(cardOne);
        if(cardTwo != null) Destroy(cardTwo);
        if(cardThree != null) Destroy(cardThree);

        List<CardSO> randomizedCards = new List<CardSO>();

        List<CardSO> availibleCards = new List<CardSO>(deck);
        availibleCards.RemoveAll(card => 
        card.isUnique && alredySelectedCards.Contains(card) || card.unlockLevel > GameManager.Instance.GetCurrentLevel());

        if(availibleCards.Count < 3)
        {
            Debug.Log("Not enough availible cards");
            return;
        }

        while (randomizedCards.Count < 3)
        {
            CardSO randomCard = availibleCards[Random.Range(0, availibleCards.Count)];
            if (!randomizedCards.Contains(randomCard))
            {
                randomizedCards.Add(randomCard);
            }
        }

        cardOne = InstantiateCard(randomizedCards[0], cardPosition1);
        cardTwo = InstantiateCard(randomizedCards[1], cardPosition2);
        cardThree = InstantiateCard(randomizedCards[2], cardPosition3);
    }

    GameObject InstantiateCard(CardSO cardSO, Transform position)
    {
        GameObject cardGo = Instantiate(cardPrefab, position.position, Quaternion.identity, position);
        Card card = cardGo.GetComponent<Card>();
        card.Setup(cardSO);
        return cardGo;
    }
    public void SelectCard(CardSO selectedCard)
    {
        if (!alredySelectedCards.Contains(selectedCard))
        {
            alredySelectedCards.Add(selectedCard);
            // Aplica el efecto de la carta seleccionada al personaje
        }
            ApplyCardEffect(selectedCard);

        GameManager.Instance.ChangeState(GameManager.GameState.Playing);
    }

    void ApplyCardEffect(CardSO selectedCard)
    {
        if (Player_Controller.Instance == null)
        {
            Debug.LogError("Player_Controller.Instance es null. Asegúrate de que esté presente en la escena.");
            return;
        }

        switch (selectedCard.effectType)
        {
            case CardEffect.DamageBoost:
                Player_Controller.Instance.IncreaseDamage(selectedCard.effectValueInt);
                break;
            case CardEffect.HealthIncrease:
                Player_Controller.Instance.IncreaseHealth(selectedCard.effectValueInt);
                break;
            case CardEffect.AttackSpeedIncrease:
                Player_Controller.Instance.IncreaseAttackSpeed(selectedCard.effectValue);
                break;
            case CardEffect.MovementSpeedIncrease:
                Player_Controller.Instance.MovementSpeedIncrease(selectedCard.effectValue);
                break;
        }
    }


    public void ShowCardSelection()
    {
        cardSelectionUI.SetActive(true);
    }
    public void HideCardSelection()
    {
        cardSelectionUI.SetActive(false);
        playerC.UpdateHealthbar(playerC.maxHealth,playerC.currentHealth);
    }
}
