using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
   	public static LevelManager Instance{ get; private set; }
	public Player Player { get; private set;}
	public CameraController Camera { get; private set; }
	public FinishLevel FinishLevel { get; private set; }
	public List<Checkpoint> _checkpoints;
	public int _currentCheckpointIndex;
	public int MaxCoins;
	public int Coins = 0;
	public float LevelTime;
	public float StartLevelTime;
	public TextMeshProUGUI EnergyTMP;
	public TextMeshProUGUI CoinsTMP;
	public TextMeshProUGUI TimeTMP;
	public TextMeshProUGUI LevelNameTMP;
	public float PlayLevelTime;
	public string RussianLevelName;
	public string EnglishLevelName;
	private int _die;
	
	public void Awake()
	{
		Instance = this;
		StartLevelTime = LevelTime;
	}

	public void Start()
	{
		_checkpoints = FindObjectsOfType<Checkpoint>().OrderBy(t => t.transform.position.x).ToList();
		_currentCheckpointIndex = _checkpoints.Count > 0 ? 0 : -1;

		Player = FindObjectOfType<Player>();
		Camera = FindObjectOfType<CameraController>();
		FinishLevel = FindObjectOfType<FinishLevel>();

		var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerRespawnListener>();

		foreach (var listener in listeners)
		{
			for (var i = _checkpoints.Count - 1; i >= 0; i--)
			{
				var distance = ((MonoBehaviour) listener).transform.position.x - _checkpoints[i].transform.position.x;
				if (distance < 0)
					continue;

				_checkpoints[i].AssignObjectToCheckpoint(listener);
				break;
			}
		}

		StartCoroutine(ShowLevelName());
	}

	public void Update()
	{
		LevelTime -= Time.deltaTime;

		//EnergyTMP.text = Player.Energy + " / " + PlayerPrefs.GetInt ("MaxEnergy");
		CoinsTMP.text  = Coins + " / " + MaxCoins;
		if (LevelTime > 0)
		{
			TimeTMP.text = Mathf.RoundToInt(LevelTime).ToString();
		}

		PlayLevelTime += Time.deltaTime;
		
		var isAtLastCheckpoint = _currentCheckpointIndex + 1 >= _checkpoints.Count;
		if (isAtLastCheckpoint)
			return;

		var distanceToNextCheckpoint = _checkpoints [_currentCheckpointIndex + 1].transform.position.x - Player.transform.position.x;
		if (distanceToNextCheckpoint >= 0)
			return;
		
		_currentCheckpointIndex++;
	}

	public void GoToNextLevel(string levelName)
	{
		StartCoroutine (GotoNextLevelCo (levelName));
	}

	private IEnumerator GotoNextLevelCo (string levelName)
	{
		yield return new WaitForSeconds (1f);

		if (string.IsNullOrEmpty (levelName))
			SceneManager.LoadScene("MainMenu");
		else
			SceneManager.LoadScene(levelName);
	}

	public void KillPlayer()
	{
        _die++;
        Debug.Log(_die);
		StartCoroutine(KillPlayerCo ());
	}
	
	public IEnumerator KillPlayerCo()
	{
		Player.Kill();
		Camera.IsFollowing = false;
		yield return new WaitForSeconds (2f);

		_checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
		Camera.IsFollowing = true;
	}

	private IEnumerator ShowLevelName()
	{
		if (PlayerPrefs.GetString("Language") == "Russian")
		{
			LevelNameTMP.text = RussianLevelName;
		}
		if (PlayerPrefs.GetString("Language") == "English")
		{
			LevelNameTMP.text = EnglishLevelName;
		}
					
			
		yield return new WaitForSeconds(2f);
		LevelNameTMP.text = "";
	}
}