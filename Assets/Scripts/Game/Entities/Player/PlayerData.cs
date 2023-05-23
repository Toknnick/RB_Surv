using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    #region LVL
    public int minXPForNewLvl;
    public int AddXPForNewLvl;
    private int xp = 0;
    private int lvl = 1;
    public int XP { get { return xp; } set { xp = value; if (xp >= minXPForNewLvl + AddXPForNewLvl * (Level -1)) { xp = 0; Level++; }; OnXPChanged?.Invoke(minXPForNewLvl + AddXPForNewLvl * Level, xp); } }
    public int Level { get { return lvl; } set { lvl = value; OnLevelChanged?.Invoke(); } }
    #endregion
    #region Steel Plates
    private int steelPlatesObtained = 0;
    public int SteelPlatesObtained { get => steelPlatesObtained; set { steelPlatesObtained = value; } }
    #endregion    
    #region Gems
    private int gems = 0;
    public int Gems { get => gems; set { gems = value; } }
    #endregion
    #region Gold
    private int gold = 0;
    public int Gold { get => gold; set { gold = value; OnGoldChanged?.Invoke(); } }
    #endregion
    #region SpecialCharge
    private int specialCharge = 0;
    public int SpecialCharge { get => specialCharge; set {specialCharge = value; if (specialCharge == 100) { OnSpecialChargeIsOn.Invoke(); specialCharge = 0; } } }
    #endregion

    public event Action OnGoldChanged;
    public event Action OnLevelChanged;
    public event Action<float, float> OnXPChanged;
    public event Action OnSpecialChargeIsOn;
}
