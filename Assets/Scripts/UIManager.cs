using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour

{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartGameText;
    private GameManager _gm;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score is: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartGameText.gameObject.SetActive(false);
        _gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    private void Update()
    {

    }

    public void PlayerScoreUpdate (int playerScore)
    {
        _scoreText.text = "Score is: " + playerScore;
    }

    public void UpdateLives (int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
        
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence ()
    {
        _gm.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartGameText.gameObject.SetActive(true);
        StartCoroutine(FlickerTextEffect());
    }

    IEnumerator FlickerTextEffect ()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(true);
        }
    }

}
