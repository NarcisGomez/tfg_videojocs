using UnityEngine;
using SimpleGraphQL;
using TMPro;
using UnityEngine.UI;

public class UserManager : MonoBehaviour
{
    private static UserManager instance;
    private string loggedUser;
    private UserStats userStats;
    [SerializeField] TMP_InputField usernameField;
    [SerializeField] TMP_InputField passwordField;
    [SerializeField] Button loginButton;
    [SerializeField] Button logoutButton;
    [SerializeField] Button statsButton;
    [SerializeField] TMP_Text username;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject loginMenu;
    [SerializeField] GameObject drumTitle;
    [SerializeField] TMP_Dropdown dropdown;

    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        string token = PlayerPrefs.GetString("refreshToken", "noToken");
        if (!(token.Equals("noToken") || token.Equals("")))
        {
            RefreshToken(token);
        }
    }

    public static UserManager GetInstance()
    {
        if (instance != null) return instance;
        throw new System.Exception("There is no Game Manager");
    }

    public string GetUser() {
        return loggedUser;
    }

    public UserStats GetUserStats()
    {
        return userStats;
    }

    public async void SignIn()
    {
        string query = $"query {{signIn(username: \"{usernameField.text}\", password: \"{passwordField.text}\") {{\naccessToken\nrefreshToken\nusername\n}}\n}}";
        var client = new GraphQLClient("https://ujtoaoadjk.execute-api.eu-west-3.amazonaws.com/prod");
        var request = new Request
        {
            Query = query
        };
        var response = await client.Send(request);
        GetSignIn credentials = JsonUtility.FromJson<SignInData>(response).data;
        SaveSignIn(credentials.signIn.accessToken, credentials.signIn.refreshToken, credentials.signIn.username);
    }

    public void LogOut()
    {
        PlayerPrefs.DeleteAll();
        loggedUser = null;
        username.text = "";
        username.gameObject.SetActive(false);
        loginButton.gameObject.SetActive(true);
    }

    public void SetDropdown()
    {
        GameManager.GetInstance().SetInstrument(dropdown.options[dropdown.value].text);
    }

    private async void GetStats()
    {
        string query = $"query {{ getUserStats(user: \"{loggedUser}\") {{\nid\nsongs\nbest\ntried\ncompleted\n}}\n}}";
        var client = new GraphQLClient("https://ujtoaoadjk.execute-api.eu-west-3.amazonaws.com/prod");
        var request = new Request
        {
            Query = query
        };
        var response = await client.Send(request);
        userStats = JsonUtility.FromJson<GetUserStatsData>(response).data.getUserStats;
        logoutButton.gameObject.SetActive(true);
        statsButton.gameObject.SetActive(true);
    }

    private async void RefreshToken(string token)
    {
        string query = $"query {{refreshAccessToken(refreshToken: \"{token}\") {{\naccessToken\nusername\n}}\n}}";
        var client = new GraphQLClient("https://ujtoaoadjk.execute-api.eu-west-3.amazonaws.com/prod");
        var request = new Request
        {
            Query = query
        };
        var response = await client.Send(request);
        RefreshToken credentials = JsonUtility.FromJson<RefreshTokenData>(response).data;
        SaveRefresh(credentials.refreshAccessToken.accessToken, credentials.refreshAccessToken.username);
    }

    private void SaveSignIn(string accessToken, string refreshToken, string user)
    {
        PlayerPrefs.SetString("accessToken", accessToken);
        PlayerPrefs.SetString("refreshToken", refreshToken);
        PlayerPrefs.SetString("username", user);
        PlayerPrefs.Save();
        ChangeView(user);
    }

    private void SaveRefresh(string accessToken, string user)
    {
        PlayerPrefs.SetString("accessToken", accessToken);
        PlayerPrefs.SetString("username", user);
        PlayerPrefs.Save();
        ChangeView(user);
        
    }

    private void ChangeView(string user)
    {
        loggedUser = user;
        loginButton.gameObject.SetActive(false);
        username.text = user;
        username.gameObject.SetActive(true);
        mainMenu.SetActive(true);
        loginMenu.SetActive(false);
        drumTitle.SetActive(true);
        GetStats();
    }
}