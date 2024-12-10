namespace Patterns.Memento;

public class Editor : IOriginator
{
    private int _editorX = 0;
    private int _editorY = 0;

    private int _fontSize = 12;
    
    public void Navigate(int ax, int ay)
    {
        _editorX += ax;
        _editorY += ay;
    }

    public void Zoom(int size)
    {
        _fontSize += size;
    }
    
    public IMemento Snapshot()
    {
        return EditorSnapshot.Capture(this);
    }

    public void Restore(IMemento snapshot)
    {
        if (snapshot is not EditorSnapshot editorSnapshot)
        {
            throw new ArgumentException("Invalid memento type");
        }
        
        _editorX = editorSnapshot.EditorX;
        _editorY = editorSnapshot.EditorY;
        _fontSize = editorSnapshot.FontSize;
    }
    
    public string PositionBar => $"({_editorX}, {_editorY}) [{_fontSize}]";
    
    private class EditorSnapshot : IMemento
    {
        public string Name { get; private init; } = null!;
        public string CreatedAt { get; private init; } = null!;

        public int EditorX { get; private init; }
        public int EditorY { get; private init; }
        public int FontSize { get; private init; }
        
        public static EditorSnapshot Capture(Editor editor)
        {
            return new EditorSnapshot
            {
                Name = "Editor Snapshot",
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),

                EditorX = editor._editorX,
                EditorY = editor._editorY,
                FontSize = editor._fontSize,
            };
        }
    }
}