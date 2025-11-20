using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetButton : MonoBehaviour
{
    [SerializeField] private PetType petType;
    [SerializeField] private Button button;


    public void AttachPet()
    {
        if (GameManager.Instance.equippedPet == petType)
        {
            GameManager.Instance.equippedPet = PetType.None;
        }
        else
        {
            GameManager.Instance.equippedPet = petType;
        }

        UpdateButtonColor();
    }
    private void UpdateButtonColor()
    {
        var colors = button.colors;
        Color equippedColor = new Color(245 / 255f, 245 / 255f, 245 / 255f, 1);
        Color unequippedColor = new Color(0, 0, 0, 1f);
        if(GameManager.Instance.equippedPet == petType)
        {
            colors.normalColor = equippedColor;
            colors.selectedColor = equippedColor;
        }
        else
        {
            colors.normalColor = unequippedColor;
            colors.selectedColor=unequippedColor;
        }
        button.colors = colors;
    }
    private void Update()
    {
        UpdateButtonColor();
    }
}
