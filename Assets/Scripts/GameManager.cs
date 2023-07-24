using UnityEngine;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance=>instance;

    protected bool soundEnabled = true;
    protected int itemNumberBommb = 0;
    protected int itemNumberHammer = 0;
    protected int numberCoin = 0;
    protected int numberBlockBest = 0;
    protected int itemNumberX2 = 0;

    public bool SoundEnabled => soundEnabled;
    public int ItemNumberBommb => itemNumberBommb;
    public int ItemNumberHammer => itemNumberHammer;
    public int NumberCoin => numberCoin;
    public int NumberBlockBest => numberBlockBest;
    public int ItemNumberX2 => itemNumberX2;
    // Hàm khởi tạo Singleton
    protected virtual void Awake()
    {
        // Kiểm tra nếu đã có một instance GameManager tồn tại thì hủy instance mới và giữ instance cũ
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Lưu instance GameManager đang hoạt động
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameSettings();
    }

    public void LoadGameSettings()
    {
        this.soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        this.itemNumberBommb = PlayerPrefs.GetInt("ItemNumberBommb", 3);
        this.itemNumberHammer = PlayerPrefs.GetInt("ItemNumberHammer", 3);
        this.numberCoin = PlayerPrefs.GetInt("NumberCoin", 500);
        this.numberBlockBest= PlayerPrefs.GetInt("NumberBlockBest", 2);
        this.itemNumberX2 = PlayerPrefs.GetInt("ItemNumberX2", 2);
    }
  
    public void SetSoundEnabled(bool soundEnabled)
    {
        this.soundEnabled = soundEnabled;
        PlayerPrefs.SetInt("SoundEnabled", soundEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void SetItemNumberBommb(int ItemNumberBommb)
    {
        this.itemNumberBommb = ItemNumberBommb;
        PlayerPrefs.SetInt("ItemNumberBommb",this.itemNumberBommb) ;
        PlayerPrefs.Save();
    }
    public void SetItemNumberHammer(int ItemNumberHammer)
    {
        this.itemNumberHammer = ItemNumberHammer;
        PlayerPrefs.SetInt("ItemNumberHammer",this.itemNumberHammer);
        PlayerPrefs.Save();
    }
    public void SetNumberCoin(int NumberCoin)
    {
        this.numberCoin = NumberCoin;
        PlayerPrefs.SetInt("NumberCoin",this.numberCoin);
        PlayerPrefs.Save();
    }
    public void SetNumberBlockBest(int NumberBlockBest)
    {
        this.numberBlockBest = NumberBlockBest;
        PlayerPrefs.SetInt("NumberBlockBest", this.numberBlockBest);
        PlayerPrefs.Save();
    }
    public void SetItemNumberX2(int ItemNumberX2)
    {
        this.itemNumberX2 = ItemNumberX2;
        PlayerPrefs.SetInt("ItemNumberX2", this.itemNumberX2);
        PlayerPrefs.Save();
    }
}

