using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftTextAnimation : MonoBehaviour
{
    public float showTime;

    private bool playingAnim;
    void Start()
    {
        
    }

    void Update()
    {
        if (playingAnim)
        {
            StartCoroutine(ShowText());
            playingAnim = false;
        }
    }

    public void StartAnim(int craftedItems, int colorIndex, int rarityIndex)
    {
        GetComponent<TextMeshProUGUI>().text = "You crafted " + craftedItems.ToString() + " items of " + colorIndex.ToString() + " color and " + rarityIndex.ToString() + " rarity!";
        playingAnim = true;
    }

    IEnumerator ShowText()
    {
        
        GetComponent<TextMeshProUGUI>().enabled = true;
        yield return new WaitForSeconds(showTime);
        GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
