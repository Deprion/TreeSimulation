using UnityEngine;

public class SunManager : MonoBehaviour
{
    [SerializeField] private GameParamSO param;
    public void Init()
    {
        DontDestroyOnLoad(this);
        EventManagerSO.inst.OnTurn += LightSpread;
    }
    private void LightSpread()
    {
        int sunLevel = param.SunLevel;
        int attempt = 0;
        for (int i = 0; i < LevelManagerSO.inst.Width; i++)
        {
            sunLevel = param.SunLevel;
            attempt = 0;
            for (int j = 0; j < LevelManagerSO.inst.Height; j++)
            {
                var pos = new Position(i, j);
                if (LevelManagerSO.inst.IsNotFullDirectly(pos))
                    continue;

                LevelManagerSO.inst.GetCellDirectly(pos).AddEnergy(sunLevel);
                sunLevel -= 3;
                attempt++;
                if (attempt >= 3) break;
            }
        }
    }
}