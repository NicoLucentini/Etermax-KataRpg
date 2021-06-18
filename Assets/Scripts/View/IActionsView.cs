using com.rpg.domain;

public interface IActionsView
{
    void UpdateCharactersView();
    void OnHeal();
    void OnAttack();
    void OnCharacterCreated(Character character);
}