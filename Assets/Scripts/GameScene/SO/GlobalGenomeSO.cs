using UnityEngine;

[CreateAssetMenu(menuName = "Static/GlobalGenome")]
public class GlobalGenomeSO : ScriptableObject
{
    public static GlobalGenomeSO inst;

    public int MinGenome, MaxGenome, AmountGenome;

    public void Init()
    {
        inst = this;
    }
}
