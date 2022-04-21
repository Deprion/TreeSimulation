using UnityEngine;

public class DNA
{
    public Genome[] Genomes { get; private set; }
    public int MaxAge { get; private set; }
    public int MutationChance { get; private set; }

    public DNA Clone()
    {
        Genome[] arr = new Genome[Genomes.Length];
        for (int i = 0; i < Genomes.Length; i++)
        {
            arr[i] = Genomes[i].Clone();
        }
        return new DNA(arr, MaxAge, MutationChance);
    }
    public void Mutate()
    {
        var GenRand = Random.Range(0, Genomes.Length);
        var NumRand = Random.Range(0, Genomes[GenRand].Numbers.Length);
        Genomes[GenRand].Numbers[NumRand] =
            Random.Range(GlobalGenomeSO.inst.MinGenome, GlobalGenomeSO.inst.MaxGenome);
    }
    public void MutateAge()
    {
        int min = MaxAge - 1 < 1 ? 1 : MaxAge - 1;
        MaxAge = Random.Range(min, MaxAge + 2);
    }
    public void MutateChance()
    {
        int min = MutationChance - 10 < 1 ? 0 : MutationChance - 10;
        int max = MutationChance + 10 > 99 ? 100 : MutationChance + 10;
        MutationChance = Random.Range(min, max + 1);
    }

    public void GenerateDNA()
    {
        Genomes = new Genome[GlobalGenomeSO.inst.AmountGenome];
        for (int i = 0; i < Genomes.Length; i++)
        {
            Genomes[i] = new Genome();
            if (i == 0) Genomes[0].GenerateRandom(2);
            else Genomes[i].GenerateRandom();

        }
        MutationChance = GameParamSO.inst.ChanceMutation;
        MaxAge = GameParamSO.inst.MaxAge;
    }
    public DNA() { }
    public DNA(Genome[] gen, int age, int mutation)
    {
        Genomes = gen;
        MaxAge = age;
        MutationChance = mutation;
    }
}
