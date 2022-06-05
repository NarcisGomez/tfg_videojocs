using UnityEngine;
using SimpleGraphQL;
using TMPro;
using UnityEngine.UI;

public class UserManager : MonoBehaviour
{
    private static UserManager instance;
    private string loggedUser;
    [SerializeField] TMP_InputField usernameField;
    [SerializeField] TMP_InputField passwordField;
    [SerializeField] Button loginButton;
    [SerializeField] TMP_Text username;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject loginMenu;
    [SerializeField] GameObject drumTitle;

    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
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

    public async void SignIn()
    {
        string query = $"query {{signIn(username: \"{usernameField.text}\", password: \"{passwordField.text}\") {{\naccessToken\nrefreshToken\nusername\n}}\n}}";
        var client = new GraphQLClient("https://ujtoaoadjk.execute-api.eu-west-3.amazonaws.com/prod");
        var request = new Request
        {
            Query = query
        };
        var response = await client.Send(request);
        GetSignIn credentials = processSignIn(response);
        SaveAndChange(credentials.signIn.accessToken, credentials.signIn.refreshToken, credentials.signIn.username);
    }

    private GetSignIn processSignIn(string data)
    {
        SignInData dt = JsonUtility.FromJson<SignInData>(data);
        return dt.data;
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
        RefreshToken credentials = processRefresh(response);
        SaveRefresh(credentials.refreshAccessToken.accessToken, credentials.refreshAccessToken.username);
    }

    private RefreshToken processRefresh(string data)
    {
        RefreshTokenData dt = JsonUtility.FromJson<RefreshTokenData>(data);
        return dt.data;
    }

    private void SaveAndChange(string accessToken, string refreshToken, string user)
    {
        PlayerPrefs.SetString("accessToken", accessToken);
        PlayerPrefs.SetString("refreshToken", refreshToken);
        PlayerPrefs.SetString("username", user);
        PlayerPrefs.Save();
        loggedUser = user;
        loginButton.gameObject.SetActive(false);
        username.text = user;
        username.gameObject.SetActive(true);
        mainMenu.SetActive(true);
        loginMenu.SetActive(false);
        drumTitle.SetActive(true);
    }

    private void SaveRefresh(string accessToken, string user)
    {
        PlayerPrefs.SetString("accessToken", accessToken);
        PlayerPrefs.SetString("username", user);
        PlayerPrefs.Save();
        loginButton.gameObject.SetActive(false);
        username.text = user;
        username.gameObject.SetActive(true);
        mainMenu.SetActive(true);
        loginMenu.SetActive(false);
        drumTitle.SetActive(true);
    }
}