using Random = UnityEngine.Random;

public class Genome
{
    public int[] Numbers { get; private set; }

    public void GenerateRandom()
    {
        Numbers = new int[4];
        int min = GlobalGenomeSO.inst.MinGenome;
        int max = GlobalGenomeSO.inst.MaxGenome;

        for (int i = 0; i < Numbers.Length; i++)
        {
            Numbers[i] = Random.Range(min, max + 1);
        }
    }

    public void GenerateRandom(int minimumGood)
    {
        Numbers = new int[4];
        int min = GlobalGenomeSO.inst.MinGenome;
        int max = GlobalGenomeSO.inst.MaxGenome / 2;

        for (int i = 0; i < Numbers.Length; i++)
        {
            Numbers[i] = Random.Range(min, max + 1);
        }

        if (RecalculateGoodAmount() < minimumGood) GenerateRandom(minimumGood);
    }
    private int RecalculateGoodAmount()
    {
        int good = 0;
        for (int i = 0; i < Numbers.Length; i++)
        {
            if (Numbers[i] <= GlobalGenomeSO.inst.AmountGenome) good++;
        }

        return good;
    }
    public Genome() { }
    public Genome(int[] array)
    {
        Numbers = array;
    }
    public override string ToString()
    {
        return $"{Numbers[0]} : {Numbers[1]} : {Numbers[2]} : {Numbers[3]}";
    }

    public Genome Clone()
    {
        int[] arr = new int[Numbers.Length];
        for (int i = 0; i < Numbers.Length; i++)
        {
            arr[i] = Numbers[i];
        }
        return new Genome(arr);
    }
}
