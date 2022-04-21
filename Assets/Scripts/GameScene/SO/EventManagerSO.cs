using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Event")]
public class EventManagerSO : ScriptableObject
{
    public static EventManagerSO inst;
    public event Action OnTurn;

    public void Init()
    {
        inst = this;
    }

    public void OnTurnInvoke()
    {
        OnTurn?.Invoke();
    }
}
