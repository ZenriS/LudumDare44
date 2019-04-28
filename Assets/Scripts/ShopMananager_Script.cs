using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMananager_Script : MonoBehaviour
{
    private PlayerInv_Script _playerInv;
    private PlayerInput_Script _playerInput;
    private Sword_Script _sword;
    public GameObject ShopUI;
    public TextMeshProUGUI BloodBankText;
    public Button[] ShopButtons;
    private List<TextMeshProUGUI> _shopButtonsTexts;
    private GameMananger_Script _gameManangerScript;
    private GameObject _infoTextScreen;
    private TextMeshProUGUI _infoText;


    [Serializable]
    public class AllItems
    {
        public string ItemName;
        [TextArea]
        public string ItemFlavourText;
        public int ItemID;
        public int ItemCost;
        public int ItemCostIncrease;
        public float ItemAmountGiven;
        public GameObject ItemPrefab;
        public int ItemDropAmount;
    }
    public AllItems[] ItemsForSale;

    public int StoreBloodAmount;
    public int StoredBlood;

	void Start ()
	{
	    _playerInv = FindObjectOfType<PlayerInv_Script>();
	    _playerInput = _playerInv.GetComponent<PlayerInput_Script>();
	    _sword = _playerInv.transform.GetComponentInChildren<Sword_Script>();
	    _gameManangerScript = _playerInv.GameManagScript;
	    _infoTextScreen = ShopButtons[0].transform.parent.parent.GetChild(2).gameObject;
	    _infoText = _infoTextScreen.GetComponentInChildren<TextMeshProUGUI>();
	    ShopUI.SetActive(false);
	    UpdateBloodBankUI();
        _shopButtonsTexts = new List<TextMeshProUGUI>();
	    SetUpButtons();
    }

    public void Item1()
    {
        /*if (StoredBlood >= ItemsForSale[0].ItemCost)
        {
            //test seed
            _playerInv.Seeds[0].SeedCount += ItemsForSale[0].ItemAmountGiven;
            _playerInv.Seeds[0].SeedName = ItemsForSale[0].ItemName;
            _playerInv.Seeds[0].SeedDrop = ItemsForSale[0].ItemPrefab;
            _playerInv.Seeds[0].SeeDropAmount = ItemsForSale[0].ItemDropAmount;
            //_playerInv.UseBlood(ItemsForSale[0].ItemCost);
            //StoredBlood -= ItemsForSale[0].ItemCost;
            
            UpdateBloodBankUI();
        }*/

        _playerInv.Seeds[0].SeedCount += (int) ItemsForSale[0].ItemAmountGiven;
        _playerInv.Seeds[0].SeedName = ItemsForSale[0].ItemName;
        _playerInv.Seeds[0].SeedDrop = ItemsForSale[0].ItemPrefab;
        _playerInv.Seeds[0].SeeDropAmount = ItemsForSale[0].ItemDropAmount;
        SpendBlood(ItemsForSale[0].ItemCost);
        _playerInv.UpdateSeedText();
        UpdateBloodBankUI();
    }

    public void StoreBlood()
    {
        _playerInv.UseBlood(StoreBloodAmount);
        StoredBlood += StoreBloodAmount;
        UpdateBloodBankUI();
    }

    public void UpgradeAttackspeed()
    {
        float a = _playerInput.UpgradeAttackSpeed(ItemsForSale[1].ItemAmountGiven);
        SpendBlood(ItemsForSale[1].ItemCost);
        ItemsForSale[1].ItemCost += ItemsForSale[1].ItemCostIncrease;
        _shopButtonsTexts[1].text = ItemsForSale[1].ItemName +"\n" + ItemsForSale[1].ItemCost;
        Debug.Log("a: " +a);
        if (a < 0.5f)
        {
            ShopButtons[1].interactable = false;
            _shopButtonsTexts[1].text = "At max";
        }
    }

    public void UpgradeFertilzeCost()
    {
        float a = _playerInv.UpgradeFertilizeCost(ItemsForSale[2].ItemAmountGiven);
        SpendBlood(ItemsForSale[2].ItemCost);
        ItemsForSale[2].ItemCost += ItemsForSale[2].ItemCostIncrease;
        _shopButtonsTexts[2].text = ItemsForSale[2].ItemName + "\n" + ItemsForSale[2].ItemCost;
        if (a < 5)
        {
            ShopButtons[2].interactable = false;
            _shopButtonsTexts[2].text = "At max";
        }
    }

    public void BuyBloodSword()
    {
        float a = _sword.IncreaseBloodgain((int) ItemsForSale[3].ItemAmountGiven);
        SpendBlood(ItemsForSale[3].ItemCost);
        ItemsForSale[3].ItemCost += ItemsForSale[3].ItemCostIncrease;
        _shopButtonsTexts[3].text = ItemsForSale[3].ItemName + "\n" + ItemsForSale[3].ItemCost;
        if (a > 6)
        {
            ShopButtons[3].interactable = false;
            _shopButtonsTexts[3].text = "At max";
        }
    }

    public void BecomeVampire()
    {
        _gameManangerScript.GameOver("Victory!",
            "You are now a creature of the night, \n and you thought you where depende on blood befor");
    }

    public void ShowInfoText(int id)
    {
        _infoTextScreen.SetActive(true);
        _infoText.text = ItemsForSale[id].ItemFlavourText;
    }

    public void HideInfoText()
    {
        _infoTextScreen.SetActive(false);
    }

    void SpendBlood(int a)
    {
        int temp = a;
        if (StoredBlood < a)
        {

            a -= StoredBlood;
            StoredBlood -= temp;
            _playerInv.UseBlood(a);
            if (StoredBlood < 0)
            {
                StoredBlood = 0;
            }
        }
        else if(StoredBlood - a >= 0)
        {
            StoredBlood -= a;
            UpdateBloodBankUI();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            ShopUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            ShopUI.SetActive(false);
        }
    }

    void SetUpButtons()
    {

        foreach (Button b in ShopButtons)
        {
            TextMeshProUGUI t = b.GetComponentInChildren<TextMeshProUGUI>();
            _shopButtonsTexts.Add(t);
        }

        int itemRef = 0;
        foreach (TextMeshProUGUI t in _shopButtonsTexts)
        {
            t.text = ItemsForSale[itemRef].ItemName + "\n" + ItemsForSale[itemRef].ItemCost;
            itemRef++;
        }
    }

    void UpdateBloodBankUI()
    {
        BloodBankText.text = "Blood Stored: \n" + StoredBlood + " l";
    }
    

}
