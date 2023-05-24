using Unity.AI.Navigation;
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
    private EntityPlayer player;

    public SpawnManager SpawnManager => spawnManager;
    public PlayerController PlayerController => playerController;
    public GameObject PlayerTransform { get => playerTransform; }
    public PlayerData PlayerData { get => playerData; }
    public PlayerAI PlayerAI { get => playerAI; }
    public EntityPlayer Player { get => player; }

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
        player = playerTransform.GetComponent<EntityPlayer>();
        playerData = playerTransform.GetComponent<PlayerData>();
        playerAI = playerTransform.GetComponent<PlayerAI>();
        playerController = Instantiate(playerController, transform.position, Quaternion.Euler(30f, 0f, 0f));
        UIPrefab = Instantiate(UIPrefab);
        playerController.TakePlayer(player);
        UIPrefab.GetComponent<Canvas>().worldCamera = GameManager.instance.PlayerController.CameraFollower.GetComponent<Camera>();

        if (playerTransform == null || player == null || playerData == null || playerAI == null || UIPrefab == null)
        {
            Debug.Log(playerTransform + "playerTransform");
            Debug.Log(player + "player");
            Debug.Log(playerData + "playerData");
            Debug.Log(playerAI + "playerAI");
            Debug.Log(UIPrefab + "UIPrefab");
        }
    }

    private void Start()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
        //spawnManager.enabled = true;
    }
}