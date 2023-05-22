using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public static User user;

    [SerializeField] private int gold;
    [SerializeField] private int gems;
    [SerializeField] private int steelPlates;


    public int Gold { get { return gold; } set { gold = value; } }
    public int Gems { get { return gems; } set { gems = value; } }
    public int Cicuits { get { return steelPlates; } set { steelPlates = value; } }

    private void Awake()
    {
        if (user != null) //Singleton: Only one instance of GameManager is allowed.
            Destroy(gameObject);
        else
            user = this;
    }

    public void Load()
    {
        Gold = PlayerPrefs.GetInt("user.gold", 0);
        Gems = PlayerPrefs.GetInt("user.gems", 0);
        Cicuits = PlayerPrefs.GetInt("user.steelPlates", 0);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("user.gold", Gold);
        PlayerPrefs.SetInt("user.gems", Gems);
        PlayerPrefs.SetInt("user.steelPlates", Cicuits);
        PlayerPrefs.Save();
    }
}
