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

        public void Attack(string actor, string target)
        {
            var characterActor = GetCharacter(actor);
            var characterTarget = GetCharacter(target);
            _attack.Execute(characterActor, characterTarget);
            _view.UpdateCharactersView();
        }

        public void Heal(string actor, string target)
        {
            var characterActor = GetCharacter(actor);
            var characterTarget = GetCharacter(target);
            _heal.Execute(characterActor, characterTarget);
            _view.UpdateCharactersView();
        }

        private Character GetCharacter(string actor)
        {
            return _characters.Find(c => c.Id == actor);
        }
    }
}