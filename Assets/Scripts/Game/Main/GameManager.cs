using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private IngameUI UIPrefab;

    private GameObject playerTransform;
    private PlayerAI playerAI;
    private PlayerData playerData;
    private Entity player;

    public SpawnManager SpawnManager => spawnManager;
    public PlayerController PlayerController => playerController;
    public GameObject PlayerTransform { get => playerTransform; }
    public PlayerData PlayerData { get => playerData; }
    public PlayerAI PlayerAI { get => playerAI; }
    public Entity Player { get => player; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
            instance = this;
        }

        //User.user.Load();

        playerTransform = Instantiate(playerPrefab, new Vector3(0, playerPrefab.transform.lossyScale.y / 2f, 0), Quaternion.identity);
        player = playerTransform.GetComponent<Entity>();
        playerData = playerTransform.GetComponent<PlayerData>();
        playerAI = playerTransform.GetComponent<PlayerAI>();
        playerController = Instantiate(playerController, transform.position, Quaternion.identity);
        UIPrefab = Instantiate(UIPrefab);
        playerController.TakePlayer(player);
        spawnManager.enabled = true;

        if (playerTransform == null || player == null || playerData == null || playerAI == null)
        {
            Debug.Log(playerTransform + "playerTransform");
            Debug.Log(player + "player");
            Debug.Log(playerData + "playerData");
            Debug.Log(playerAI + "playerAI");
            Debug.Log(UIPrefab + "UIPrefab");
        }
    }
}