using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameParams")]
public class GameParamSO : ScriptableObject
{
    [SerializeField] private int sunLevel;
    private WaitForSeconds[] waitForSecArr = new WaitForSeconds[]
    {
        new WaitForSeconds(4),
        new WaitForSeconds(2),
        new WaitForSeconds(1),
        new WaitForSeconds(0.5f),
        new WaitForSeconds(0.25f),
        new WaitForSeconds(0.15f),
        new WaitForSeconds(0.1f)
    };
    private int CurrentSpeed = 3;
    public static GameParamSO inst;
    public int ChanceMutation { get { return Global.MutationChance; } }
    public int MaxAge { get { return Global.MaxAge; } }
    public int SunLevel
    {
        get 
        {
            return sunLevel;
        }
        private set 
        {
            sunLevel = value;
        }
    }
    public WaitForSeconds WaitForUpdate = new WaitForSeconds(0.2f);
    public WaitForSeconds WaitForSec
    {
        get
        {
            return waitForSecArr[CurrentSpeed - 1];
        }
    }
    public event Action<int> OnSpeedChange;
    public void Init()
    {
        inst = this;
    }
    public void OnSpeedChangeInvoke()
    {
        OnSpeedChange?.Invoke(CurrentSpeed);
    }
    public void ChangeSpeed(bool isPositive)
    {
        if (isPositive)
        {
            int temp = CurrentSpeed + 1;
            CurrentSpeed = temp < 7 ? temp : 7;
        }
        else
        {
            int temp = CurrentSpeed - 1;
            CurrentSpeed = temp > 1 ? temp : 1;
        }

        OnSpeedChange?.Invoke(CurrentSpeed);
    }
}
