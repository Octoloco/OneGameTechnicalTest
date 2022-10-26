using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combinations", menuName = "ScriptableObjects/Combinations", order = 3)]
public class CombinationsScriptableObject : ScriptableObject
{
    public Product[] products = new Product[3];

    [System.Serializable]
    public struct Product
    {
        public int rarityID;
        public int[] materials;
    }
}
