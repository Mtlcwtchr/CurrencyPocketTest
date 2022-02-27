using System.Collections.Generic;
using System.Globalization;
using DefaultNamespace;
using Pocket;
using Pocket.Abstraction;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    [SerializeField] private Button addGemsButton;
    [SerializeField] private Button withdrawGemsButton;
    [SerializeField] private Button addCoinsButton;
    [SerializeField] private Button withdrawCoinsButton;
    [SerializeField] private Text gemsTextField;
    [SerializeField] private Text coinsTextField;

    private readonly ICurrencyPocket<float> _currencyPocket = CurrencyPocket.Instance;


    private void OnEnable()
    {
        _currencyPocket.Initialize(new CurrencyPocketConfiguration()
        {
            OnCurrencyUpdated = OnCurrencyUpdated,
            SupportedCurrencyTypes = new List<string>(){ "gems", "coins"}
        }, new PlayerPrefsCurrencyPocketStateHandler());
        
        addGemsButton.onClick.AddListener(OnAddGemsButtonClick);
        withdrawGemsButton.onClick.AddListener(OnWithdrawGemsButtonClick);
        addCoinsButton.onClick.AddListener(OnAddCoinsButtonClick);
        withdrawCoinsButton.onClick.AddListener(OnWithdrawCoinsButtonClick);

        _currencyPocket.GetCurrency("gems", out float gemsValue);
        UpdateTextFieldWithValue(ref gemsTextField, gemsValue);
        
        _currencyPocket.GetCurrency("coins", out float coinsValue);
        UpdateTextFieldWithValue(ref coinsTextField, coinsValue);
    }


    private void OnDisable()
    {
        addGemsButton.onClick.RemoveListener(OnAddGemsButtonClick);
        withdrawGemsButton.onClick.RemoveListener(OnWithdrawGemsButtonClick);
        addCoinsButton.onClick.RemoveListener(OnAddCoinsButtonClick);
        withdrawCoinsButton.onClick.RemoveListener(OnWithdrawCoinsButtonClick);
        
        _currencyPocket.Deinitialize();
    }


    private void OnAddGemsButtonClick()
    {
        _currencyPocket.AddCurrency("gems", 100.0f);
    }

    private void OnWithdrawGemsButtonClick()
    {
        _currencyPocket.AddCurrency("gems", -100.0f);
    }

    private void OnAddCoinsButtonClick()
    {
        _currencyPocket.AddCurrency("coins", 100.0f);
    }

    private void OnWithdrawCoinsButtonClick()
    {
        _currencyPocket.AddCurrency("coins", -100.0f);
    }

    
    private void OnCurrencyUpdated(string currency, float value)
    {
        switch (currency)
        {
            case "gems":
                UpdateTextFieldWithValue(ref gemsTextField, value);
                break;
            
            case "coins":
                UpdateTextFieldWithValue(ref coinsTextField, value);
                break;
            
            default:
                Debug.LogWarning("!NonFatal! Unsupported currency type updated.");
                break;
        }
    }

    
    private void UpdateTextFieldWithValue(ref Text textField, float value)
    {
        textField.text = value.ToString(CultureInfo.InvariantCulture);
    }
}
