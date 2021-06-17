using System.Collections.Generic;
using Actions;
using com.rpg.domain;
using NSubstitute;
using NUnit.Framework;
using Presenter;

namespace Editor.Test.Presenter
{
    public class CharactersActionsPresenterTests
    {
        private IActionsView _actionsView;
        private  CharactersActionsPresenter _charactersActionsPresenter;
        private Character _actor = new Character("Actor");
        private Character _target = new Character("Target");
        private Attack _attack = Substitute.For<Attack>();
        private Heal _heal = Substitute.For<Heal>();

        [SetUp]
        public void SetUp()
        {
            _actionsView = Substitute.For<IActionsView>();
            _charactersActionsPresenter = new CharactersActionsPresenter(new List<Character>{_actor, _target}, _attack, _heal, _actionsView);
        }
        
        [Test]
        public void ViewIsUpdatedOnCreation()
        {
           _actionsView.Received(1).UpdateCharactersView();
        }

        [Test]
        public void OnAttackDoActionAndUpdateView()
        {
            _actionsView.ClearReceivedCalls();
            
            _charactersActionsPresenter.Attack("Actor", "Target");
            _attack.Received(1).Execute(_actor, _target);
            _actionsView.Received(1).UpdateCharactersView();
        }
        
        [Test]
        public void OnHealDoActionAndUpdateView()
        {
            _actionsView.ClearReceivedCalls();
            
            _charactersActionsPresenter.Attack("Actor", "Target");
            _heal.Received(1).Execute(_actor, _target);
            _actionsView.Received(1).UpdateCharactersView();
            
        }
    }
}