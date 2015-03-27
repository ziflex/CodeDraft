namespace CodeDraft.Editor.Contracts
{
    public interface IComponent<T>
    {
        T Model { get; set; }
    }
}