
public interface IPlayerProp
{
    Player player { get; }

    void Equip(Player newPlayer);
    void UnEquip();
    void EnableProp(bool enable);
}
