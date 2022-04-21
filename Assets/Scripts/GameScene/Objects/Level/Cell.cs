using UnityEngine;

public class Cell : MonoBehaviour
{
    public Position key;
    private SpriteRenderer ImageOfCell;
    public Global.TypeOfCell Fullnes;
    private CellOfTree cellTree;

    private void Awake()
    {
        ImageOfCell = GetComponent<SpriteRenderer>();
        SetEmpty();
    }
    public CellOfTree GetCell()
    {
        return cellTree;
    }
    public void ChangeColor(Color32 color)
    {
        ImageOfCell.color = color;
    }
    public void AddEnergy(int amount)
    {
        cellTree.AddEnergy(amount);
    }
    public void SetActive()
    {
        ImageOfCell.color = Color.white;
    }
    public void SetInActive(Color32 color)
    {
        ImageOfCell.color = color;
    }
    public void SetFullnes(Color32 color, CellOfTree cellTree)
    {
        Fullnes = Global.TypeOfCell.Full;
        ImageOfCell.sprite = RenderSO.inst.FullSprite;
        ImageOfCell.color = color;
        this.cellTree = cellTree;
    }
    public void SetFullnes(Color32 color)
    {
        Fullnes = Global.TypeOfCell.Busy;
        ImageOfCell.sprite = RenderSO.inst.FullSprite;
        ImageOfCell.color = color;
    }
    public void SetEmpty()
    {
        Fullnes = Global.TypeOfCell.Empty;
        ImageOfCell.sprite = RenderSO.inst.EmptySprite;
        ImageOfCell.color = Color.grey;
    }
}
