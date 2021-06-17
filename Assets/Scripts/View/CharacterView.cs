using com.rpg.domain;
using TMPro;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _aliveText;
    [SerializeField] private string _name;
    public Character Character { get; private set; }

    private void Start()
    {
        _nameText.text = _name;
        Character = new Character(_name);
    }

    public void UpdateView()
    {
        _nameText.text = _name;
        _healthText.text = Character.Health.ToString();
        _levelText.text = Character.Level.ToString();
        _aliveText.text = Character.Alive.ToString();
    }
}
