using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BallView : MonoBehaviour, IPointerClickHandler
{
    private Image _image;
    [SerializeField] private Text _scoreText;

    private BallSettings _settings;
    public BallSettings Settings
    {
        get { return _settings; }
        set { _settings = value; }
    }

    public Action<BallView> ReturnToPool { get; set; }
    public Action<int> AddPoints { get; set; }

    private bool _isShowScore = false;
    private const float SCORE_SHOW_TIME = 1;
    private const int SCORE_ANIM_SPEED = 50;
    private float _scoreShowingTimeLeft;

    private int _currentSpeed = 0;

    public void Init()
    {
        if (_image == null)
        {
            _image = gameObject.GetComponent<Image>();
        }
        if (_image.enabled == false)
        {
            _image.enabled = true;
        }

        _image.rectTransform.sizeDelta = new Vector2(_settings.Size, _settings.Size);
        _image.color = _settings.Color;
        _image.rectTransform.localPosition = _settings.StartPos;
        _currentSpeed = _settings.Speed;

        _scoreText.enabled = false;
        _isShowScore = false;
    }

    private void Update()
    {
        if (GD.GAME_PAUSED) return;
        if (_settings == null) return;

        if (_isShowScore)
        {
            _image.enabled = false;
            _scoreShowingTimeLeft -= Time.deltaTime;
            if (_scoreShowingTimeLeft <= 0)
            {
                ReturnToPool(this);
                _isShowScore = false;
            }
        }

        Vector3 pos = _image.rectTransform.localPosition;
        if (pos.y > GD.ScreenHeight/2 + _settings.Size)
        {
            ReturnToPool(this);
            return;
        }

        pos.y += _currentSpeed * Time.deltaTime;
        _image.rectTransform.localPosition = pos;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowScore();
        if (AddPoints != null)
        {
            AddPoints(_settings.Points);
        }
    }

    private void ShowScore()
    {
        _scoreText.text = string.Format("+{0}", _settings.Points);
        _scoreText.enabled = true;
        _scoreShowingTimeLeft = SCORE_SHOW_TIME;
        _isShowScore = true;
        _currentSpeed = SCORE_ANIM_SPEED;
    }
}

public class BallSettings
{
    public int Size { get; set; }
    public int Speed { get; set; }
    public Color Color { get; set; }
    public Vector3 StartPos { get; set; }
    public int Points { get; set; }
}

