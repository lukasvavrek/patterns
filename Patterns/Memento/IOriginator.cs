namespace Patterns.Memento;

public interface IOriginator
{
    IMemento Snapshot();
    void Restore(IMemento snapshot);
}