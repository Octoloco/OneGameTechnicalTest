using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombinationManager : MonoBehaviour
{
    public static CombinationManager Instance;

    [SerializeField] private TMP_InputField itemsSelectedText;
    [SerializeField] private InventoryScriptableObject rawInventory;
    [SerializeField] private ProductsScriptableObject productInventory;
    [SerializeField] private Slider itemSlider;
    [SerializeField] private CombinationsScriptableObject redCombinations;
    [SerializeField] private CombinationsScriptableObject greenCombinations;
    [SerializeField] private CombinationsScriptableObject purpleCombinations;
    [SerializeField] private CombinationsScriptableObject yellowCombinations;
    [SerializeField] private CombinationsScriptableObject cyanCombinations;
    [SerializeField] private CombinationsScriptableObject blueCombinations;
    [SerializeField] private CraftTextAnimation craftText;
    [SerializeField] private ParticleSystemRenderer[] particles = new ParticleSystemRenderer[4];
    [SerializeField] private Material[] particleMaterials = new Material[6];

    private int selectionIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        productInventory.SetItems();
    }

    public void UpdateValues()
    {
        int numberOfItems = CheckMaterialAvailability(RotationManager.instance.GetColorIndex(), RotationManager.instance.GetRarityIndex());

        if (numberOfItems > 0)
        {
            itemSlider.maxValue = numberOfItems;
            itemSlider.minValue = 1;
        }
        else
        {
            itemSlider.maxValue = 0;
            itemSlider.minValue = 0;
        }
        itemSlider.value = itemSlider.maxValue;
        UpdateText();
    }

    public void UpdateText()
    {
        itemsSelectedText.text = itemSlider.value.ToString();
    }

    public void UpdateSlider()
    {
        int newValue;
        int.TryParse(itemsSelectedText.text, out newValue);
        if (newValue < 0)
        {
            itemSlider.value = 0;
            itemsSelectedText.text = "0";
        }
        else if (newValue > itemSlider.maxValue)
        {
            itemSlider.value = itemSlider.maxValue;
            itemsSelectedText.text = itemSlider.maxValue.ToString();
        }
        else
        {
            itemSlider.value = newValue;
        }
    }

    public void AddSelectedItems()
    {
        itemSlider.value++; 
    }

    public void SubtractSelectedItems()
    {
        itemSlider.value--;
    }

    public void SelectButton()
    {
        switch (selectionIndex)
        {
            case 0:
                selectionIndex++;
                SelectColor();
                break;

            case 1:
                SelectRarity();
                break;
        }
    }

    public void BackButton()
    {
        selectionIndex--;
        RotationManager.instance.ChangeWheel();
    }

    private void SelectColor()
    {
        RotationManager.instance.ChangeWheel();
    }

    private void SelectRarity()
    {
        int colorIndex = RotationManager.instance.GetColorIndex();
        int rarityIndex = RotationManager.instance.GetRarityIndex();
        Craft(colorIndex, rarityIndex);
    }

    private void Craft(int colorIndex, int rarityIndex)
    {
        int craftedItems = RotationManager.instance.GetCraftedItems();
        switch (colorIndex)
        {
            case 0:
                for (int i = 0; i < redCombinations.products[rarityIndex].materials.Length; i++)
                {
                    rawInventory.items[redCombinations.products[rarityIndex].materials[i]] -= craftedItems;
                }
                productInventory.AddItems(craftedItems, colorIndex, rarityIndex);
                craftText.StartAnim(craftedItems, colorIndex, rarityIndex);
                break;

            case 1:
                for (int i = 0; i < greenCombinations.products[rarityIndex].materials.Length; i++)
                {
                    rawInventory.items[greenCombinations.products[rarityIndex].materials[i]] -= craftedItems;
                }
                productInventory.AddItems(craftedItems, colorIndex, rarityIndex);
                craftText.StartAnim(craftedItems, colorIndex, rarityIndex);
                break;

            case 2:
                for (int i = 0; i < purpleCombinations.products[rarityIndex].materials.Length; i++)
                {
                    rawInventory.items[purpleCombinations.products[rarityIndex].materials[i]] -= craftedItems;
                }
                productInventory.AddItems(craftedItems, colorIndex, rarityIndex);
                craftText.StartAnim(craftedItems, colorIndex, rarityIndex);
                break;

            case 3:
                for (int i = 0; i < yellowCombinations.products[rarityIndex].materials.Length; i++)
                {
                    rawInventory.items[yellowCombinations.products[rarityIndex].materials[i]] -= craftedItems;
                }
                productInventory.AddItems(craftedItems, colorIndex, rarityIndex);
                craftText.StartAnim(craftedItems, colorIndex, rarityIndex);
                break;

            case 4:
                for (int i = 0; i < cyanCombinations.products[rarityIndex].materials.Length; i++)
                {
                    rawInventory.items[cyanCombinations.products[rarityIndex].materials[i]] -= craftedItems;
                }
                productInventory.AddItems(craftedItems, colorIndex, rarityIndex);
                craftText.StartAnim(craftedItems, colorIndex, rarityIndex);
                break;

            case 5:
                for (int i = 0; i < blueCombinations.products[rarityIndex].materials.Length; i++)
                {
                    rawInventory.items[blueCombinations.products[rarityIndex].materials[i]] -= craftedItems;
                }
                productInventory.AddItems(craftedItems, colorIndex, rarityIndex);
                craftText.StartAnim(craftedItems, colorIndex, rarityIndex);
                break;
        }
    }

    public int GetSelectionIndex()
    {
        return selectionIndex;
    }

    private int CheckMaterialAvailability(int colorIndex, int rarityIndex)
    {
        foreach(ParticleSystemRenderer p in particles)
        {
            p.GetComponent<ParticleSystem>().Stop();
        }
        int maxCraftables = 0;
        switch (colorIndex)
        {
            case 0:
                maxCraftables = rawInventory.items[redCombinations.products[rarityIndex].materials[0]];
                particles[0].material = particleMaterials[redCombinations.products[rarityIndex].materials[0]];
                particles[0].GetComponent<ParticleSystem>().Play();
                for (int i = 1; i < redCombinations.products[rarityIndex].materials.Length; i++) 
                {
                    particles[i].material = particleMaterials[redCombinations.products[rarityIndex].materials[i]];
                    particles[i].GetComponent<ParticleSystem>().Play();
                    if (rawInventory.items[redCombinations.products[rarityIndex].materials[i]] >= 1)
                    {
                        if (rawInventory.items[redCombinations.products[rarityIndex].materials[i]] < maxCraftables)
                        {
                            maxCraftables = rawInventory.items[redCombinations.products[rarityIndex].materials[i]];
                        }
                    }
                    else
                    {
                        maxCraftables = 0;
                        break;
                    }
                }
                break;

            case 1:
                maxCraftables = rawInventory.items[greenCombinations.products[rarityIndex].materials[0]];
                particles[0].material = particleMaterials[greenCombinations.products[rarityIndex].materials[0]];
                particles[0].GetComponent<ParticleSystem>().Play();
                for (int i = 1; i < greenCombinations.products[rarityIndex].materials.Length; i++)
                {
                    particles[i].material = particleMaterials[greenCombinations.products[rarityIndex].materials[i]];
                    particles[i].GetComponent<ParticleSystem>().Play();
                    if (rawInventory.items[greenCombinations.products[rarityIndex].materials[i]] >= 1)
                    {
                        if (rawInventory.items[greenCombinations.products[rarityIndex].materials[i]] < maxCraftables)
                        {
                            maxCraftables = rawInventory.items[greenCombinations.products[rarityIndex].materials[i]];
                        }
                    }
                    else
                    {
                        maxCraftables = 0;
                        break;
                    }
                }
                break;

            case 2:
                maxCraftables = rawInventory.items[purpleCombinations.products[rarityIndex].materials[0]];
                particles[0].material = particleMaterials[purpleCombinations.products[rarityIndex].materials[0]];
                particles[0].GetComponent<ParticleSystem>().Play();
                for (int i = 1; i < purpleCombinations.products[rarityIndex].materials.Length; i++)
                {
                    particles[i].material = particleMaterials[purpleCombinations.products[rarityIndex].materials[i]];
                    particles[i].GetComponent<ParticleSystem>().Play();
                    if (rawInventory.items[purpleCombinations.products[rarityIndex].materials[i]] >= 1)
                    {
                        if (rawInventory.items[purpleCombinations.products[rarityIndex].materials[i]] < maxCraftables)
                        {
                            maxCraftables = rawInventory.items[purpleCombinations.products[rarityIndex].materials[i]];
                        }
                    }
                    else
                    {
                        maxCraftables = 0;
                        break;
                    }
                }
                break;

            case 3:
                maxCraftables = rawInventory.items[yellowCombinations.products[rarityIndex].materials[0]];
                particles[0].material = particleMaterials[yellowCombinations.products[rarityIndex].materials[0]];
                particles[0].GetComponent<ParticleSystem>().Play();
                for (int i = 1; i < yellowCombinations.products[rarityIndex].materials.Length; i++)
                {
                    particles[i].material = particleMaterials[yellowCombinations.products[rarityIndex].materials[i]];
                    particles[i].GetComponent<ParticleSystem>().Play();
                    if (rawInventory.items[yellowCombinations.products[rarityIndex].materials[i]] >= 1)
                    {
                        if (rawInventory.items[yellowCombinations.products[rarityIndex].materials[i]] < maxCraftables)
                        {
                            maxCraftables = rawInventory.items[yellowCombinations.products[rarityIndex].materials[i]];
                        }
                    }
                    else
                    {
                        maxCraftables = 0;
                        break;
                    }
                }
                break;

            case 4:
                maxCraftables = rawInventory.items[cyanCombinations.products[rarityIndex].materials[0]];
                particles[0].material = particleMaterials[cyanCombinations.products[rarityIndex].materials[0]];
                particles[0].GetComponent<ParticleSystem>().Play();
                for (int i = 1; i < cyanCombinations.products[rarityIndex].materials.Length; i++)
                {
                    particles[i].material = particleMaterials[cyanCombinations.products[rarityIndex].materials[i]];
                    particles[i].GetComponent<ParticleSystem>().Play();
                    if (rawInventory.items[cyanCombinations.products[rarityIndex].materials[i]] >= 1)
                    {
                        if (rawInventory.items[cyanCombinations.products[rarityIndex].materials[i]] < maxCraftables)
                        {
                            maxCraftables = rawInventory.items[cyanCombinations.products[rarityIndex].materials[i]];
                        }
                    }
                    else
                    {
                        maxCraftables = 0;
                        break;
                    }
                }
                break;

            case 5:
                maxCraftables = rawInventory.items[blueCombinations.products[rarityIndex].materials[0]];
                particles[0].material = particleMaterials[blueCombinations.products[rarityIndex].materials[0]];
                particles[0].GetComponent<ParticleSystem>().Play();
                for (int i = 1; i < blueCombinations.products[rarityIndex].materials.Length; i++)
                {
                    particles[i].material = particleMaterials[blueCombinations.products[rarityIndex].materials[i]];
                    particles[i].GetComponent<ParticleSystem>().Play();
                    if (rawInventory.items[blueCombinations.products[rarityIndex].materials[i]] >= 1)
                    {
                        if (rawInventory.items[blueCombinations.products[rarityIndex].materials[i]] < maxCraftables)
                        {
                            maxCraftables = rawInventory.items[blueCombinations.products[rarityIndex].materials[i]];
                        }
                    }
                    else
                    {
                        maxCraftables = 0;
                        break;
                    }
                }
                break;
        }
        return maxCraftables;
    }
}
