using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RotationManager : MonoBehaviour
{
    public static RotationManager instance;

    [SerializeField] private Transform externalWheel;
    [SerializeField] private Transform internalWheel;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button moreButton;
    [SerializeField] private Button lessButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject inputSliderGroup;
    [SerializeField] private Slider itemSlider;
    [SerializeField] private TMP_InputField itemInput;
    [SerializeField] private int rotationSpeed;

    private int divisionsInWheel;
    private int colorIndex = 0;
    private int rarityIndex = 0;
    private float degreesBetweenRotation = 0;
    private Quaternion currentRotation = Quaternion.Euler(Vector3.zero);
    private Quaternion nextRotation = Quaternion.Euler(Vector3.zero);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        int index;
        if (CombinationManager.Instance.GetSelectionIndex() == 0)
        {
            index = colorIndex;
        }
        else
        {
            index = rarityIndex;
        }

        externalWheel.GetChild(index).GetComponent<Button>().interactable = true;
        divisionsInWheel = externalWheel.childCount;
        degreesBetweenRotation = 360 / divisionsInWheel;
        CombinationManager.Instance.UpdateValues();
    }

    private void Update()
    {
        if (currentRotation != nextRotation)
        {
            externalWheel.rotation = Quaternion.Lerp(currentRotation, nextRotation, rotationSpeed * Time.deltaTime);
            currentRotation = externalWheel.rotation;
            CombinationManager.Instance.UpdateValues();

            if (Mathf.Abs(currentRotation.eulerAngles.z - nextRotation.eulerAngles.z) <= .2f)
            {
                externalWheel.rotation = Quaternion.Euler(externalWheel.rotation.eulerAngles.x, externalWheel.rotation.eulerAngles.y, Mathf.Round(externalWheel.rotation.eulerAngles.z));
                currentRotation = externalWheel.rotation;
                nextRotation = currentRotation;
            }
        }
        else
        {
            int index;
            if (CombinationManager.Instance.GetSelectionIndex() == 0)
            {
                index = colorIndex;
            }
            else
            {
                index = rarityIndex;
            }

            externalWheel.GetChild(index).GetComponent<Button>().interactable = true;
            SwitchInteractState(true);
        }
    }

    public void RotateWheelLeft()
    {
        if (CombinationManager.Instance.GetSelectionIndex() == 0)
        {
            externalWheel.GetChild(colorIndex).GetComponent<Button>().interactable = false;
            colorIndex--;
            if (colorIndex < 0)
            {
                colorIndex = divisionsInWheel - 1;
            }
        }
        else
        {
            externalWheel.GetChild(rarityIndex).GetComponent<Button>().interactable = false;
            rarityIndex--;
            if (rarityIndex < 0)
            {
                rarityIndex = divisionsInWheel - 1;
            }
        }

        currentRotation = externalWheel.rotation;
        float nextZDegree = currentRotation.eulerAngles.z - degreesBetweenRotation;
        nextZDegree = Mathf.Round(nextZDegree);
        nextRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, nextZDegree);
        SwitchInteractState(false);
    }

    public void RotateWheelRight()
    {
        if (CombinationManager.Instance.GetSelectionIndex() == 0)
        {
            externalWheel.GetChild(colorIndex).GetComponent<Button>().interactable = false;
            colorIndex++;
            if (colorIndex > divisionsInWheel - 1)
            {
                colorIndex = 0;
            }
        }
        else
        {
            externalWheel.GetChild(rarityIndex).GetComponent<Button>().interactable = false;
            rarityIndex++;
            if (rarityIndex > divisionsInWheel - 1)
            {
                rarityIndex = 0;
            }
        }

        currentRotation = externalWheel.rotation;
        float nextZDegree;
        nextZDegree = currentRotation.eulerAngles.z + degreesBetweenRotation;
        nextZDegree = Mathf.Round(nextZDegree);
        nextRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, nextZDegree);
        SwitchInteractState(false);
    }

    public void SwitchInteractState(bool state)
    {
        leftButton.interactable = state;
        rightButton.interactable = state;
        selectButton.interactable = state;
        moreButton.interactable = state;
        lessButton.interactable = state;
        itemSlider.interactable = state;
        itemInput.interactable = state;
        backButton.interactable = state;
    }

    public int GetColorIndex()
    {
        return colorIndex;
    }

    public int GetRarityIndex()
    {
        return rarityIndex;
    }

    public void ChangeWheel()
    {
        Transform temp = externalWheel;
        externalWheel = internalWheel;
        internalWheel = temp;
        divisionsInWheel = externalWheel.childCount;
        degreesBetweenRotation = 360 / divisionsInWheel;

        if (CombinationManager.Instance.GetSelectionIndex() == 0)
        {
            backButton.gameObject.SetActive(false);
            inputSliderGroup.SetActive(false);
        }
        else
        {
            backButton.gameObject.SetActive(true);
            inputSliderGroup.SetActive(true);
        }
    }

    public int GetCraftedItems()
    {
        return (int)itemSlider.value;
    }

}
