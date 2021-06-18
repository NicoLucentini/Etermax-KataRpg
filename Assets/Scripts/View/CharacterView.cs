using System;
using com.rpg.domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _aliveText;
    [SerializeField] private TextMeshProUGUI _rangeText;
    [SerializeField] private TextMeshProUGUI _factionsText;
    [SerializeField] private string _name;
    public Character Character { get; private set; }


    private void Awake()
    {
        ActionsView.onChangeIteration += OnChangeIteration;
        _nameText.text = _name;
        Character = new Character(_name);
    }


    private void OnChangeIteration(int iteration)
    {
        _rangeText.transform.parent.gameObject.SetActive(iteration > 1);
        _factionsText.transform.parent.gameObject.SetActive(iteration > 2);
    }
    public void SetCharacter(Character character)
    {
        Character = character;
    }

    public void UpdateView()
    {
        _nameText.text = _name;
        _healthText.text = Character.Health.ToString();
        _levelText.text = Character.Level.ToString();
        _aliveText.text = Character.Alive.ToString();
        _rangeText.text = Character.Range.ToString();
        _factionsText.text = Character.FactionsToString();
    }
   
}
