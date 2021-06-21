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

    [SerializeField]private  IterationData[] iterationData;

    [Header("Character Creation")]
    [SerializeField] private GameObject _characterViewPrefab;
    [SerializeField] private Button _createCharacter;
    [SerializeField] private TMP_InputField _characterName;
    [SerializeField] private TMP_InputField _characterHealth;
    [SerializeField] private TMP_InputField _characterLevel;
    [SerializeField] private Toggle _characterRanged;

    [Header("Character Factions")]
    [SerializeField] private Button _joinFactionBtn;
    [SerializeField] private TMP_InputField _faction;
    [SerializeField] private Button _leaveFactionBtn;

    void Awake() {
        var iterations = new List<string>(){ "Iteration 1", "Iteration2", "Iteration3", "Iteration4" };
        _iterationsDropdown.AddOptions(iterations);
        _attack.onClick.AddListener(OnAttack);
        _heal.onClick.AddListener(OnHeal);
        _createCharacter.onClick.AddListener(OnCreateCharacter);
        _joinFactionBtn.onClick.AddListener(OnJoinFaction);
        _leaveFactionBtn.onClick.AddListener(OnLeaveFaction);
    }

    private void Start()
    {
        //_presenter = new CharactersActionsPresenter(_characterViews.Select(view=> view.Character).ToList(), new Attack(), new Heal(), this);

        _presenter = new CharactersActionsPresenter(_characterViews.Select(view => view.Character).ToList(), new Attack(), new Heal(), this);
        OnChangeIteration(0);
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
        if(string.IsNullOrEmpty(_distance.text))
            _presenter.Attack(GetActorName(), GetTargetName());
        else
            _presenter.Attack(GetActorName(), GetTargetName(), int.Parse(_distance.text));
    }
    public void OnJoinFaction() {
        Debug.LogFormat("Char {0} Join Faction {1}", GetActorName(), _faction.text);
        _presenter.JoinFaction(GetActorName(), _faction.text);
    }
    public void OnLeaveFaction(){

        Debug.LogFormat("Char {0} Leaves Faction {1}", GetActorName(), _faction.text);
        _presenter.LeaveFaction(GetActorName(), _faction.text);
    }
    public void OnCreateCharacter() {
        CharacterData characterData = new CharacterData(
            _characterName.text,
            _characterLevel.text,
            _characterHealth.text,
            _characterRanged.isOn
            );
        _presenter.CreateCharacter(characterData);
    }
    public void OnChangeIteration(int iteration) {

        CleanCharacterViews();
        _distance.text = "";
        _distance.transform.parent.gameObject.SetActive(iteration > 1);
        _faction.transform.parent.gameObject.SetActive(iteration > 2);
        _joinFactionBtn.gameObject.SetActive(iteration > 2);
        _leaveFactionBtn.gameObject.SetActive(iteration > 2);

        onChangeIteration?.Invoke(iteration);

        if (iteration < iterationData.Length)
        {
            foreach (var characterData in iterationData[iteration].characterDatas)
                _presenter.CreateCharacter(characterData);
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

    //interface
    public void OnCharacterCreated(Character character)
    {
        CharacterView characterUI = GameObject.Instantiate(_characterViewPrefab).GetComponent<CharacterView>();
        characterUI.transform.SetParent(_charactersViewParent);
        _characterViews.Add(characterUI);
        characterUI.SetCharacter(character);
    }

    public void UpdateCharacterView(Character character)
    {
        Debug.Log(character.FactionsToString());
        _characterViews.Find(c => c.Character.Id == character.Id)?.UpdateView();
    }
}
[System.Serializable]
public struct CharacterData{
    public string name;
    public string level;
    public string health;
    public bool isRanged;

    public CharacterData(string name, string level, string health, bool isRanged)
    {
        this.name = name;
        this.level = level;
        this.health = health;
        this.isRanged = isRanged;
    }
}
[System.Serializable]
public class IterationData {
    public List<CharacterData> characterDatas;

}