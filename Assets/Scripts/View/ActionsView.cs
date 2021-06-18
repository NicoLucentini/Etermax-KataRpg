using System.Collections.Generic;
using System.Linq;
using Actions;
using com.rpg.domain;
using Presenter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionsView : MonoBehaviour, IActionsView
{
    [SerializeField] private Button _attack;
    [SerializeField] private Button _heal;
    [SerializeField] private TMP_InputField _actor;
    [SerializeField] private TMP_InputField _target;

    [SerializeField] private TMP_InputField _distance;

    [SerializeField] private List<CharacterView> _characterViews;
    private CharactersActionsPresenter _presenter;
    public static System.Action<int> onChangeIteration;

    [SerializeField]private Dropdown _iterationsDropdown;

    [SerializeField] private Transform _charactersViewParent;
 
    [Header("Character Creation")]
    [SerializeField] private Button _createCharacter;
    [SerializeField] private TMP_InputField _characterName;
    [SerializeField] private TMP_InputField _characterHealth;
    [SerializeField] private TMP_InputField _characterLevel;
    [SerializeField] private Toggle _characterRanged;

    [SerializeField] private Button _joinFactionBtn;
    [SerializeField] private TMP_InputField _faction;
    [SerializeField] private Button _leaveFactionBtn;

    [SerializeField] private GameObject _characterViewPrefab;

    void Awake() {
        var iterations = new List<string>(){ "Iteration 1", "Iteration2", "Iteration3", "Iteration4" };
        _iterationsDropdown.AddOptions(iterations);
    }

    private void Start()
    {
        _presenter = new CharactersActionsPresenter(_characterViews.Select(view=> view.Character).ToList(), new Attack(), new Heal(), this);        
        _attack.onClick.AddListener(OnAttack);
        _heal.onClick.AddListener(OnHeal);
        _createCharacter.onClick.AddListener(OnCreateCharacter);
    }

    public void UpdateCharactersView()
    {
        _characterViews.ForEach(v => v.UpdateView());
    }

    public void OnHeal()
    {
        Debug.LogFormat("Char {0} Healing char {1}", GetActorName(), GetTargetName());
        _presenter.Heal(GetActorName(), GetTargetName());
    }

    public void OnAttack()
    {
        Debug.LogFormat("Char {0} Attacking char {1}", GetActorName(), GetTargetName());
        _presenter.Attack(GetActorName(), GetTargetName());
    }

    public void OnCreateCharacter() {
        CharacterData characterData = new CharacterData(
            _characterName.text,
            int.Parse(_characterLevel.text),
            int.Parse(_characterHealth.text),
            _characterRanged.isOn
            );

        _presenter.CreateCharacter(characterData);
    }
    public void OnChangeIteration(int iteration) {

        CleanCharacterViews();
        _distance.transform.parent.gameObject.SetActive(iteration > 1);
        onChangeIteration?.Invoke(iteration);
        if (iteration == 1) {
            _presenter.CreateCharacter(new CharacterData("Player1", 7, 1000, true));
            _presenter.CreateCharacter(new CharacterData("Player2", 1, 1000, true));
           
        }
        else if (iteration == 2)
        {
            _presenter.CreateCharacter(new CharacterData("Ranged", 1, 1000, true));
            _presenter.CreateCharacter(new CharacterData("Melee", 1, 1000, false));
        }
        

        _presenter = new CharactersActionsPresenter(_characterViews.Select(view => view.Character).ToList(), new Attack(), new Heal(), this);
    }

    public void CleanCharacterViews() {
        for (int i = 0; i < _characterViews.Count; i++) {
            Destroy(_characterViews[i].gameObject);
        }
        _characterViews.Clear();
    }
    private string GetActorName()
    {
        return _actor.text;
    }

    private string GetTargetName()
    {
        return _target.text;
    }

    public void OnCharacterCreated(Character character)
    {
        CharacterView characterUI = GameObject.Instantiate(_characterViewPrefab).GetComponent<CharacterView>();
        characterUI.transform.SetParent(_charactersViewParent);
        _characterViews.Add(characterUI);
        characterUI.SetCharacter(character);
    }
}
public struct CharacterData{
    public string name;
    public int level;
    public int health;
    public bool isRanged;

    public CharacterData(string name, int level, int health, bool isRanged)
    {
        this.name = name;
        this.level = level;
        this.health = health;
        this.isRanged = isRanged;
    }
}