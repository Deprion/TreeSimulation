using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/LevelManager")]
public class LevelManagerSO : ScriptableObject
{
    public Dictionary<Position, Cell> Cells = new Dictionary<Position, Cell>();
    [SerializeField] private GameObject CellPrefab, Root;
    [SerializeField] private int height, width;
    [SerializeField] private float between;
    [SerializeField] private float offset = 1.15f;
    public float minY { get; private set; }
    public int Height 
    { 
        get 
        { 
            return height;
        }
        private set
        {
            height = value;

        } 
    }
    public int Width
    {
        get
        {
            return width;
        }
        private set
        {
            width = value;

        }
    }
    public float Between
    {
        get
        {
            return between;
        }
        private set
        {
            Between = value;

        }
    }
    public static LevelManagerSO inst;

    public void Init()
    {
        inst = this;
    }
    public void GenerateWorld()
    {
        if (Cells != null) Cells.Clear();

        height = Global.Height;
        width = Global.Width;
        float lengthX = 0.3f;
        float lengthY = 0.3f;
        float posX = -(Width / 2 * (lengthX * offset));
        float posY = Height / 2 * lengthY;

        float rowY = posY;

        var root = Instantiate(Root).transform;
        for (int i = 0; i < height; i++)
        {
            double rowX = posX;
            for (int j = 0; j < width; j++)
            {
                var obj = Instantiate(CellPrefab, root, false);
                var cell = obj.GetComponent<Cell>();
                var pos = new Position(j, i);

                cell.key = pos;
                Cells.Add(pos, cell);

                obj.transform.localPosition = new Vector2((float)rowX, rowY);
                rowX += lengthX + between;
            }
            rowY -= lengthY + between;
        }
        minY = rowY;
    }
    private int CheckX(int x)
    { 
        return x == -1 ? width - 1 : x == width ? 0 : x;
    }
    public Cell GetCell(Position key)
    {
        Cell cell;
        key.X = CheckX(key.X);
        Cells.TryGetValue(key, out cell);
        return cell;
    }
    public Cell GetCellDirectly(Position key)
    {
        key.X = CheckX(key.X);
        return Cells[key];
    }
    public bool IsEmpty(Position key)
    {
        Cell cell;
        key.X = CheckX(key.X);
        Cells.TryGetValue(key, out cell);
        if (cell == null) return false;
        if (cell.Fullnes == Global.TypeOfCell.Empty) return true;
        return false;
    }
    public bool IsEmptyDirectly(Position key)
    {
        key.X = CheckX(key.X);
        if (Cells[key].Fullnes == Global.TypeOfCell.Empty) return true;
        return false;
    }
    public bool IsNotFullDirectly(Position key)
    {
        key.X = CheckX(key.X);
        if (Cells[key].Fullnes == Global.TypeOfCell.Empty
            || Cells[key].Fullnes == Global.TypeOfCell.Busy) return true;
        return false;
    }
}
