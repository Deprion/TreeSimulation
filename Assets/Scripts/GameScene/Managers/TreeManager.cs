using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    private List<Tree> trees = new List<Tree>();
    private List<Seed> seeds = new List<Seed>();
    private int absoluteAge = 0, milleAge = 0;

    public void AddSeed(Seed seed)
    {
        seeds.Add(seed);
    }
    public void RemoveSeed(Seed seed)
    {
        seeds.Remove(seed);
    }
    public void AddTree(Cell cell)
    {
        trees.Add(new Tree(cell));
        CalculateAvg();
    }
    public void AddTree(Cell cell, Color32 color)
    {
        trees.Add(new Tree(cell, color));
        CalculateAvg();
    }
    public void AddTree(Tree tree)
    {
        trees.Add(tree);
        CalculateAvg();
    }
    public void AddTree(int amount)
    {
        if (amount == 1)
        {
            AddTree(LevelManagerSO.inst.Cells[new Position(LevelManagerSO.inst.Width / 2,
                LevelManagerSO.inst.Height - 1)]);
            return;
        }
        var pos = new Position();
        int width = 0;
        int adding = LevelManagerSO.inst.Width / amount;
        int height = LevelManagerSO.inst.Height - 1;
        pos.Y = height;
        for (int i = 0; i < amount; i++)
        {
            pos.X = width;
            AddTree(LevelManagerSO.inst.Cells[pos], RenderSO.inst.GetRandomColor());
            width += adding;
        }
    }
    private void CalculateAvg()
    {
        if (trees.Count < 1) return;
        float age = 0;
        float mutation = 0;
        foreach (var tree in trees)
        {
            age += tree.Dna.MaxAge;
            mutation += tree.Dna.MutationChance;
        }
        if (trees.Count > 0)
        UIManager.inst.AvgInfoUpdate(mutation / trees.Count, age / trees.Count);
    }
    public void DelTree(Tree tree)
    {
        trees.Remove(tree);
        CalculateAvg();
    }

    public void Terminate()
    {
        absoluteAge = 0;
        milleAge = 0;
        foreach (var tree in trees)
        {
            tree.Terminate();
        }
        foreach (var seed in seeds)
        {
            seed.Terminate();
        }
        trees.Clear();
        seeds.Clear();
    }
    public void Grow()
    {
        CalculateAge();

        EventManagerSO.inst.OnTurnInvoke();

        absoluteAge++;
    }
    private void CalculateAge()
    {
        if (absoluteAge >= 1000)
        {
            milleAge++;
            absoluteAge -= 1000;
        }
        UIManager.inst.OnGrowInvoke(milleAge, absoluteAge);
    }
}
