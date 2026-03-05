using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SimpleStockMarket : MonoBehaviour
{
    [System.Serializable]
    public class Company
    {
        public string companyName;
        public float currentPrice = 100f;

        [Header("UI")]
        public TMP_Text priceText;
        public TMP_Text changeText;
        public TMP_Text ownedText;
        public Button buyButton;
        public Button sellButton;

        [HideInInspector] public int ownedShares = 0;

        private Queue<float> priceHistory = new Queue<float>();

        public void RecordPrice()
        {
            priceHistory.Enqueue(currentPrice);

            // Keep 60 seconds of history (6 updates at 10 sec each)
            if (priceHistory.Count > 50)
                priceHistory.Dequeue();
        }

        public float GetPrice60SecondsAgo()
        {
            if (priceHistory.Count < 6)
                return currentPrice;

            return priceHistory.Peek();
        }
    }

    [Header("Companies")]
    public Company[] companies;

    [Header("References")]
    public CurrencyManager currencyManager;

    [Header("Market Settings")]
    public float updateInterval = 10f;
    public float minChangePercent = -5f;
    public float maxChangePercent = 5f;

    [Header("Portfolio UI")]
    public TMP_Text portfolioValueText;

    void Start()
    {
        foreach (Company company in companies)
        {
            company.buyButton.onClick.AddListener(() => BuyStock(company));
            company.sellButton.onClick.AddListener(() => SellStock(company));

            company.RecordPrice();
            UpdateCompanyUI(company);
        }

        StartCoroutine(UpdateMarket());
    }

    IEnumerator UpdateMarket()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateInterval);

            foreach (Company company in companies)
            {
                float percentChange = Random.Range(minChangePercent, maxChangePercent);
                float changeAmount = company.currentPrice * (percentChange / 100f);

                company.currentPrice += changeAmount;

                // Prevent negative stock price
                if (company.currentPrice < 1f)
                    company.currentPrice = 1f;

                company.RecordPrice();

                UpdateCompanyUI(company);
                UpdatePortfolioValue();
            }
        }
    }

    void BuyStock(Company company)
    {
        int cost = Mathf.RoundToInt(company.currentPrice);

        if (currencyManager.Spend(cost))
        {

            company.ownedShares++;
            UpdateCompanyUI(company);
            UpdatePortfolioValue();
        }
    }

    void SellStock(Company company)
    {
        if (company.ownedShares > 0)
        {
            company.ownedShares--;
            int sellValue = Mathf.RoundToInt(company.currentPrice);
            currencyManager.Add(sellValue);

            UpdateCompanyUI(company);
            UpdatePortfolioValue();
        }
    }

    void UpdateCompanyUI(Company company)
    {
        float oldPrice = company.GetPrice60SecondsAgo();
        float difference = company.currentPrice - oldPrice;

        company.priceText.text =
            company.companyName + " Price: $" + company.currentPrice.ToString("F2");

        string sign = difference >= 0 ? "+" : "";
        company.changeText.text =
            " " + sign + difference.ToString("F2");

        company.ownedText.text =
            "Owned: " + company.ownedShares;
    }

    void UpdatePortfolioValue()
    {
        float totalValue = 0f;

        foreach (Company company in companies)
        {
            totalValue += company.currentPrice * company.ownedShares;
        }

        portfolioValueText.text =
            "Portfolio Value: $" + totalValue.ToString("F2");
    }

    public void TriggerMarketCrash()
    {
        Debug.Log("MARKET CRASH TRIGGERED!");

        foreach (Company company in companies)
        {
            // Random crash between -20% and -50%
            float crashPercent = Random.Range(20f, 50f);

            float crashAmount = company.currentPrice * (crashPercent / 100f);
            company.currentPrice -= crashAmount;

            // Prevent price going below 1
            if (company.currentPrice < 1f)
                company.currentPrice = 1f;

            company.RecordPrice();
            UpdateCompanyUI(company);
        }

        UpdatePortfolioValue();
    }
}