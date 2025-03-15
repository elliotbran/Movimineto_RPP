using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public Sprite cardImage;//Image of the card

    public string cardText;//Description of the card
    public CardEffect effectType;//Effect of the card
    //public float effectValueFloat;//Value of the effect (10%)
    public int effectValueInt;
    public float effectValue;
    public bool isUnique;//If is unique (Puede salir mas de una vez o no)
    public int unlockLevel;//Se entiende
}

public enum CardEffect
{
    DamageBoost,

    HealthIncrease,

    AttackSpeedIncrease,

    MovementSpeedIncrease
}
