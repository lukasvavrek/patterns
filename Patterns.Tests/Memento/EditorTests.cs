using Patterns.Memento;

namespace Patterns.Tests.Memento;

public class EditorTests
{
    [Test]
    public void Snapshot_IsNotNull()
    {
        var snapshot = new Editor().Snapshot();
        
        Assert.That(snapshot, Is.Not.Null);
    }
    
    [Test]
    public void Snapshot_HasCorrectMementoInfo()
    {
        var snapshot = new Editor().Snapshot();
        
        Assert.Multiple(() =>
        {
            Assert.That(snapshot.Name, Is.EqualTo("Editor Snapshot"));
            var parsedDate = DateTime.Parse(snapshot.CreatedAt);
            Assert.That(parsedDate.Date, Is.EqualTo(DateTime.Today));
        });
    }
    
    [Test]
    public void PositionBar_HasCorrectValue()
    {
        var editor = new Editor();
        editor.Navigate(10, 20);
        editor.Zoom(5);
        
        Assert.That(editor.PositionBar, Is.EqualTo("(10, 20) [17]"));
    }
    
    [Test]
    public void Snapshot_Action_Restore_Story()
    {
        var editor = new Editor();
        editor.Navigate(10, 20);
        editor.Zoom(5);
        
        Assert.That(editor.PositionBar, Is.EqualTo("(10, 20) [17]"));

        var snapshot = editor.Snapshot();
        
        editor.Navigate(10, 20);
        editor.Zoom(5);
        Assert.That(editor.PositionBar, Is.EqualTo("(20, 40) [22]"));
        
        editor.Restore(snapshot);
        Assert.That(editor.PositionBar, Is.EqualTo("(10, 20) [17]"));
    }

    [Test]
    public void Restore_InvalidMemento_Throws()
    {
        var editor = new Editor();
        
        Assert.Throws<ArgumentException>(() => editor.Restore(new Mock<IMemento>().Object));
    }
}