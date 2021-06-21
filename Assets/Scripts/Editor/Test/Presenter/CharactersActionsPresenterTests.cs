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
        public void ViewIsUpdatedOnJoinFaction()
        {
            _charactersActionsPresenter.JoinFaction("Actor", "Orcs");
            _actionsView.Received(1).UpdateCharacterView(_actor);
        }

        [Test]
        public void ViewIsUpdatedOnLeaveFaction()
        {
            _charactersActionsPresenter.JoinFaction("Actor", "Orcs");
            _charactersActionsPresenter.LeaveFaction("Actor", "Orcs");
            _actionsView.Received(2).UpdateCharacterView(_actor);
        }

        [Test]
        public void ViewIsUpdatedOnCharacterCreation()
        {
            _charactersActionsPresenter.CreateCharacter(new CharacterData("Melee", "", "", false));
            _actionsView.Received(1).OnCharacterCreated(_charactersActionsPresenter.GetCharacter("Melee"));
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

        [Test]
        public void OnCreateRangeFighter() {
            _charactersActionsPresenter.CreateCharacter(new CharacterData("Range", Character.MIN_LVL.ToString(), Character.MIN_LVL.ToString(), true));
            Assert.AreEqual(20, _charactersActionsPresenter.GetCharacter("Range").Range);
        }
        [Test]
        public void OnCreateMeleeFighter()
        {
            _charactersActionsPresenter.CreateCharacter(new CharacterData("Melee", Character.MIN_LVL.ToString(), Character.MIN_LVL.ToString(), false));
            Assert.AreEqual(2, _charactersActionsPresenter.GetCharacter("Melee").Range);
        }
        [Test]
        public void CreateCharacterWithEmptyLevelAndHealth()
        {
            _charactersActionsPresenter.CreateCharacter(new CharacterData("Melee", "", "", false));
            Assert.AreEqual(Character.MAX_HEALTH, _charactersActionsPresenter.GetCharacter("Melee").Health);
            Assert.AreEqual(Character.MIN_LVL, _charactersActionsPresenter.GetCharacter("Melee").Level);
        }
    }
}