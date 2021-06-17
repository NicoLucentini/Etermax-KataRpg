using System.Collections.Generic;
using System.Linq;
using Actions;
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
    [SerializeField] private List<CharacterView> _characterViews;
    private CharactersActionsPresenter _presenter;

    private void Start()
    {
        _presenter = new CharactersActionsPresenter(_characterViews.Select(view=> view.Character).ToList(), new Attack(), new Heal(), this);        
        _attack.onClick.AddListener(OnAttack);
        _heal.onClick.AddListener(OnHeal);
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

    private string GetActorName()
    {
        return _actor.text;
    }

    private string GetTargetName()
    {
        return _target.text;
    }
}