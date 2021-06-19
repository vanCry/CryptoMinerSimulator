using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    private int id;
    private string nameCalculator;
    private float hp;   
    private Dictionary<CurrencyCrypto, float> Hashrates;
}



public enum CurrencyCrypto 
{
    Bitcoin,
    Litecoin,
    Ethereum,
    Monero,
    Ravencoin,
    Dogecoin,
    Ripple,
    IOTA,
    NEO,
    NEM,
    Dash,
    ZCash
}
