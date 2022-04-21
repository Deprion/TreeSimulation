using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public event Action<int, int> OnGrow;
    public static UIManager inst;
    [SerializeField] private TMP_Text ageTxt, milleTxt, speedTxt,
        genomeNumTxt, leftTxt, topTxt, rightTxt, botTxt, 
        baseMutationTxt, avgMutationTxt, avgAgeTxt, mutationTxt, maxAgeTxt, energyTxt;
    [SerializeField] private GameObject menuPanel;

    private void Awake()
    {
        inst = this;
        OnGrow += UpdateAge;
        GameParamSO.inst.OnSpeedChange += UpdateSpeed;
        GameParamSO.inst.OnSpeedChangeInvoke();
        baseMutationTxt.text = GameParamSO.inst.ChanceMutation.ToString();
    }

    public void Menu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    public void OnGrowInvoke(int mille, int age)
    {
        OnGrow?.Invoke(mille, age);
    }

    public void GenomeUpdate(Genome gen, int mutation, int age, int maxAge, int energy)
    {
        leftTxt.text = gen.Numbers[0].ToString();
        topTxt.text = gen.Numbers[1].ToString();
        rightTxt.text = gen.Numbers[2].ToString();
        botTxt.text = gen.Numbers[3].ToString();
        mutationTxt.text = mutation.ToString();
        maxAgeTxt.text = $"{age}/{maxAge}";
        energyTxt.text = energy.ToString();
    }
    public void AvgInfoUpdate(float mutation, float age)
    {
        avgMutationTxt.text = $"{mutation:f1}";
        avgAgeTxt.text = $"{age:f1}";
    }

    private void UpdateAge(int mille, int age)
    {
        milleTxt.text = mille + "M";
        ageTxt.text = age.ToString();
    }

    private void UpdateSpeed(int speed)
    {
        speedTxt.text = speed.ToString();
    }
}
