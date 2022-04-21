using UnityEngine;

public class Seed
{
    private DNA dna;
    private Color32 color;
    private int energy;
    private Cell cell;
    private bool isMutated = false;

    private void Turn()
    {
        cell.SetEmpty();
        var key = cell.key;
        key.Y += 1;
        var tempCell = LevelManagerSO.inst.GetCell(key);
        if (tempCell == null) Born();
        else if (tempCell.Fullnes != Global.TypeOfCell.Empty) Dead();
        else
        { 
            cell = tempCell;
            if (isMutated)
                cell.SetFullnes(Color.blue);
            else
                cell.SetFullnes(Color.red);
        }
    }
    private void Born()
    {
        GameManager.inst.Trees.AddTree(new Tree(cell, energy, color, dna));
        Dead();
    }
    private void Dead()
    {
        GameManager.inst.Trees.RemoveSeed(this);
        EventManagerSO.inst.OnTurn -= Turn;
    }
    private void Mutate()
    {
        if (Random.Range(0, 101) < dna.MutationChance)
        {
            dna = dna.Clone();
            dna.Mutate();
            isMutated = true;
            if (Random.Range(0, 101) < dna.MutationChance)
            {
                dna.MutateAge();
                dna.MutateChance();
            }
        }
    }
    public void Terminate()
    {
        cell.SetEmpty();
        EventManagerSO.inst.OnTurn -= Turn;
    }
    public Seed(Cell _cell, DNA _dna, Color32 _color, int _energy)
    {
        cell = _cell;
        dna = _dna;
        color = _color;
        energy = _energy > 400 ? 400 : _energy;
        EventManagerSO.inst.OnTurn += Turn;
        Mutate();
        if (isMutated)
            cell.SetFullnes(Color.blue);
        else
            cell.SetFullnes(Color.red);
    }
}
