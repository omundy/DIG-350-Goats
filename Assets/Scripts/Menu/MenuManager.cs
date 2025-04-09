using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public Button hostButton;
    public TMP_Text joinCodeText;
    public TMP_InputField joinCodeInput;
    public Button joinButton;
    public TMP_Text playerCountText;
    public Button startGameButton;
    public Button startLeverGameButton;

    [SerializeField] private OurNetwork ourNetwork;
    [SerializeField] private LobbyManager lobbyManager;

    private void Start()
    {
        if (ourNetwork == null)
        {
            ourNetwork = FindObjectOfType<OurNetwork>();
            if (ourNetwork == null)
            {
                Debug.LogError("ourNetwork not found in the scene!");
                return;
            }
        }

        ourNetwork.Initialize(this);
        lobbyManager.Initialize(this, ourNetwork);

        hostButton.onClick.AddListener(lobbyManager.HostGame);
        joinButton.onClick.AddListener(() => lobbyManager.JoinGame(joinCodeInput.text));
        startGameButton.onClick.AddListener(lobbyManager.StartGame);
        startGameButton.gameObject.SetActive(false);
        startLeverGameButton.onClick.AddListener(lobbyManager.StartLeverGame);
        playerCountText.gameObject.SetActive(false);
    }

    public void UpdateJoinCodeDisplay(string code)
    {
        joinCodeText.text = $"Join Code: {code}";
    }

    public void UpdatePlayerCountDisplay(int currentPlayers, int maxPlayers)
    {
		if (playerCountText == null) return;
        playerCountText.text = $"Players: {currentPlayers}/{maxPlayers}";
        playerCountText.gameObject.SetActive(true);
    }

    public void ShowStartButton(bool show)
    {
        startGameButton.gameObject.SetActive(show);
    }
}
