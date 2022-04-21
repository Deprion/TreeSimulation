using UnityEngine;

public class CellOfTree
{
    public Genome CurrentGenome;
    public Cell CurrentCell;
    public Tree tree { get; private set; }
    public int InnerEnergy;

    public void AddEnergy(int amount)
    {
        InnerEnergy += amount;
        tree.AddEnergy(amount);
    }

    public void Dead()
    {
        CurrentCell.SetEmpty();
    }
    public CellOfTree(Genome genome, Cell cell, Tree parent, Color32 color)
    {
        CurrentGenome = genome;
        CurrentCell = cell;
        CurrentCell.SetFullnes(color, this);
        CurrentCell.SetActive();
        tree = parent;
    }
}
