using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    [SerializeField] private GameObject prefab;
    public DNA Dna = new DNA();
    public Color32 ColorOfTree;
    public int age;
    public int energy;
    private List<CellOfTree> inActiveCells = new List<CellOfTree>();
    private List<CellOfTree> activeCells = new List<CellOfTree>();

    public void AddEnergy(int amount)
    {
        energy += amount;
    }
    public void Grow()
    {
        if (SubstractEnergy()) return;
        if (AddAge()) return;
        GrowCells();
    }
    private bool SubstractEnergy()
    {
        energy -= (inActiveCells.Count + activeCells.Count) * 10;
        if (energy <= 0)
        {
            Dead();
            return true;
        }
        return false;
    }
    private bool AddAge()
    {
        age++;
        if (age >= Dna.MaxAge)
        {
            Dead();
            return true;
        }
        return false;
    }
    private void GrowCells()
    {
        foreach (var cell in activeCells.ToArray())
        {
            for (int i = 0; i < cell.CurrentGenome.Numbers.Length; i++)
            {
                if (cell.CurrentGenome.Numbers[i] < GlobalGenomeSO.inst.AmountGenome
                    && cell.InnerEnergy >= 14)
                {
                    var key = cell.CurrentCell.key;
                    switch (i)
                    {
                        case 0:
                            key.X -= 1;
                            if (LevelManagerSO.inst.IsEmpty(key))
                                AddCell(Dna.Genomes[cell.CurrentGenome.Numbers[i]], key);
                            break;
                        case 1:
                            key.Y -= 1;
                            if (LevelManagerSO.inst.IsEmpty(key))
                                AddCell(Dna.Genomes[cell.CurrentGenome.Numbers[i]], key);
                            break;
                        case 2:
                            key.X += 1;
                            if (LevelManagerSO.inst.IsEmpty(key))
                                AddCell(Dna.Genomes[cell.CurrentGenome.Numbers[i]], key);
                            break;
                        case 3:
                            key.Y += 1;
                            if (LevelManagerSO.inst.IsEmpty(key))
                                AddCell(Dna.Genomes[cell.CurrentGenome.Numbers[i]], key);
                            break;
                    }
                }
            }
            if (cell.InnerEnergy < 14) continue;
            activeCells.Remove(cell);
            cell.CurrentCell.SetInActive(ColorOfTree);
            inActiveCells.Add(cell);
        }
    }
    private void AddCell(Genome gen, Position key)
    {
        var cell = LevelManagerSO.inst.GetCellDirectly(key);
        activeCells.Add(new CellOfTree(gen, cell, this, ColorOfTree));
    }
    public void Dead()
    {
        if (activeCells.Count > 0)
        {
            int energyForCell = 0;
            if (energy > 0) energyForCell = energy / activeCells.Count;
            else energyForCell = 14;
            foreach (var cell in activeCells)
            {
                cell.Dead();
                GameManager.inst.Trees.AddSeed(new Seed(cell.CurrentCell, Dna,
                    ColorOfTree, energyForCell));
            }
        }
        foreach (var cell in inActiveCells)
        {
            cell.Dead();
        }
        EventManagerSO.inst.OnTurn -= Grow;
        GameManager.inst.Trees.DelTree(this);
    }

    public void Terminate()
    {
        foreach (var cell in activeCells)
        {
            cell.Dead();
        }
        foreach (var cell in inActiveCells)
        {
            cell.Dead();
        }
        EventManagerSO.inst.OnTurn -= Grow;
    }
    public Tree(Cell cell)
    {
        age = 0;
        energy = 300;
        ColorOfTree = Color.green;
        Dna.GenerateDNA();
        activeCells.Add(new CellOfTree(Dna.Genomes[0], cell, this, ColorOfTree));
        EventManagerSO.inst.OnTurn += Grow;
    }
    public Tree(Cell cell, Color32 color)
    {
        age = 0;
        energy = 300;
        ColorOfTree = color;
        Dna.GenerateDNA();
        activeCells.Add(new CellOfTree(Dna.Genomes[0], cell, this, ColorOfTree));
        EventManagerSO.inst.OnTurn += Grow;
    }
    public Tree(Cell cell, int energ, Color32 color, DNA dna)
    {
        age = 0;
        energy = energ;
        ColorOfTree = color;
        Dna = dna;
        activeCells.Add(new CellOfTree(Dna.Genomes[0], cell, this, ColorOfTree));
        EventManagerSO.inst.OnTurn += Grow;
    }
}
