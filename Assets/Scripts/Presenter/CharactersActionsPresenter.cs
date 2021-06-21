using System;
using System.Collections.Generic;
using Actions;
using com.rpg.domain;

namespace Presenter
{
    public class CharactersActionsPresenter
    {
        private readonly List<Character> _characters;
        private readonly Attack _attack;
        private readonly Heal _heal;
        private readonly IActionsView _view;

        public CharactersActionsPresenter(List<Character> characters, Attack attack, Heal heal, IActionsView view)
        {
            _characters = characters;
            _attack = attack;
            _heal = heal;
            _view = view;
            
            _view.UpdateCharactersView();
        }
      

        //Sobrecargado por iteracion 3 lo dejo por los test
        public void Attack(string actor, string target)
        {
            Attack(actor, target, 0);
        }
        
        public void Attack(string actor, string target, int distance)
        { 
            var characterActor = GetCharacter(actor);
            var characterTarget = GetCharacter(target);
            _attack.Execute(characterActor, characterTarget, distance);
            _view.UpdateCharactersView();
        }

        public void Heal(string actor, string target)
        {
            var characterActor = GetCharacter(actor);
            var characterTarget = GetCharacter(target);
            _heal.Execute(characterActor, characterTarget);
            _view.UpdateCharactersView();
        }
        public void CreateCharacter(CharacterData data) {
            Character character;
            int lvl = string.IsNullOrEmpty(data.level) ? Character.MIN_LVL : int.Parse(data.level);
            int health = string.IsNullOrEmpty(data.health) ? Character.MAX_HEALTH : int.Parse(data.health);

            if (data.isRanged)
                character = new RangeFighter(data.name,  health, lvl);
            else
                character = new MeleeFighter(data.name, health, lvl);

            _characters.Add(character);
            _view.OnCharacterCreated(character);
        }
        public Character GetCharacter(string actor)
        {
            return _characters.Find(c => c.Id == actor);
        }

        public void LeaveFaction(string actor, string faction)
        {
            Character character = GetCharacter(actor);
            character.LeavesFaction(faction);
            _view.UpdateCharacterView(character);
        }
        public void JoinFaction(string actor, string faction)
        {
            Character character = GetCharacter(actor);
            character.JoinFaction(faction);
            _view.UpdateCharacterView(character);
        }
    }
}