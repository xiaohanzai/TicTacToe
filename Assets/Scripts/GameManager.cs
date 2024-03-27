using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int _i;
    private string[] _playerCharacters = { "O", "X" };

    public ButtonController[] Buttons;
    public GameObject[] Strikes;

    private Dictionary<string, GameObject> _strikeDict = new Dictionary<string, GameObject>();

    public TextMeshProUGUI TextMeshPro;
    public GameObject ResetButton;

    private void Awake()
    {
        // Ensure there's only one instance of GameManager
        if (Instance == null)
        {
            Instance = this; // Set the instance to this object if it's null
            DontDestroyOnLoad(gameObject); // Optional: Keep GameManager alive between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var strike in Strikes)
        {
            _strikeDict.Add(strike.name, strike);
        }
        
        GameSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        string s = CheckWinLose();
        HandleWinLose(s);
    }

    public void GameSetUp()
    {
        foreach (var b in Buttons)
        {
            b.ResetText();
        }
        foreach (var strike in Strikes)
        {
            strike.SetActive(false);
        }
        TextMeshPro.text = "TicTacToe";
        ResetButton.SetActive(false);
        _i = 0;
    }

    public string NextClickCharacter()
    {
        return _playerCharacters[_i % 2];
    }

    public void CounterPlusOne()
    {
        _i++;
    }

    private string CheckWinLose()
    {
        int i;
        string s;

        // check horizontal strikes
        string[] cs = { "U", "M", "D" };
        for (i = 0; i < 3; i++)
        {
            s = Buttons[3 * i].textMeshPro.text;
            if (s != "" && s == Buttons[3 * i + 1].textMeshPro.text && s == Buttons[3 * i + 2].textMeshPro.text)
            {
                _strikeDict["HorizontalStrike" + cs[i]].SetActive(true);
                return s;
            }
        }

        // check vertical strikes
        cs[0] = "L";
        cs[2] = "R";
        for (i = 0; i < 3; i++)
        {
            s = Buttons[i].textMeshPro.text;
            if (s != "" && s == Buttons[i + 3].textMeshPro.text && s == Buttons[i + 6].textMeshPro.text)
            {
                _strikeDict["VerticalStrike" + cs[i]].SetActive(true);
                return s;
            }
        }

        // check cross strikes
        i = 0;
        s = Buttons[i].textMeshPro.text;
        if (s != "" && s == Buttons[i + 4].textMeshPro.text && s == Buttons[i + 8].textMeshPro.text)
        {
            _strikeDict["CrossStrike2"].SetActive(true);
            return s;
        }
        i = 2;
        s = Buttons[i].textMeshPro.text;
        if (s != "" && s == Buttons[i + 2].textMeshPro.text && s == Buttons[i + 4].textMeshPro.text)
        {
            _strikeDict["CrossStrike1"].SetActive(true);
            return s;
        }

        // check if nobody wins
        if (_i == 9)
        {
            return "n";
        }

        // game still needs to go on
        return "";
    }

    private void HandleWinLose(string s)
    {
        // game still needs to go on
        if (s == "")
        {
            TextMeshPro.text = "Player " + _playerCharacters[_i % 2] + "'s turn";
            return;
        }

        // win or lose
        if (s == "O" || s == "X")
        {
            TextMeshPro.text = "Player " + s + " won!";
        }
        else
        {
            TextMeshPro.text = "Nobody won; Play again";
        }
        ResetButton.SetActive(true);
    }
}
