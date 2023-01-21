namespace Examples;

using BasicGUI;

using VRender;

using OpenTK.Mathematics;
public class ExampleTable
{
    public static void Run()
    {
        RenderSettings settings = new RenderSettings(){
            Size = new Vector2i(800, 600),
            VSync = true,
            BackgroundColor = 0x000000FF,
            TargetFrameTime = 1/60f,
            WindowTitle = "Table example",
        };
        IRender render = RenderUtils.CreateIdealRenderOrDie(settings);

        IRenderShader? shader = render.LoadShader("gui", out var err);
        if(shader is null){
            throw new Exception("lol shader no work", err);
        }
        var texture = render.LoadTexture("ascii.png", out err);
        if(texture is null)throw new Exception("can't load ascii.png", err);
        
        RenderFont font = new RenderFont(texture, shader);

        //Create the thing that connects BasicGUI and Render together so they can talk to each other.
        IDisplay display = new RenderDisplay(font);
        //a BasicGUIPlane is the main class responsible for, well, BasicGUI. You could make a RootNode directly, but I advise against it.
        BasicGUIPlane plane = new BasicGUIPlane(800, 600, display);
        MarginContainer margin = new MarginContainer(plane.GetRoot(), 20);
        //First, we want to put a table on the left center.
        LayoutContainer leftCenter = new LayoutContainer(margin, VAllign.center, HAllign.left);
        ColorBackgroundElement bg = new ColorBackgroundElement(margin, 0x333333ff, 0);
        //Add the table as well.
        TableContainer table = new TableContainer((container) => {return new ColorOutlineRectElement(container, 0xaaaaaaff, null, null, null, null, 5, 1);}, leftCenter, 2, 10);
        //Add the elements to the table
        int fontSize = 15;
        uint textColor = 0xffffffff;
        new TextElement(table, textColor,  fontSize, "Fruit ",         font, display, 0);
        new TextElement(table, textColor,  fontSize, "Color ",         font, display, 0);
        new TextElement(table, 0xffff00ff, fontSize, "Banana ",        font, display, 0);
        new TextElement(table, 0xffff00ff, fontSize, "Yellow ",        font, display, 0);
        new TextElement(table, 0xff0000ff, fontSize, "Apple ",         font, display, 0);
        new TextElement(table, 0xff0000ff, fontSize, "Red ",           font, display, 0);
        new TextElement(table, 0xff3366ff, fontSize, "Dragonfruit ",   font, display, 0);
        new TextElement(table, 0xff3366ff, fontSize, "Red ",           font, display, 0);
        new TextElement(table, 0xff33ccff,  fontSize, "Mango ",        font, display, 0);
        new TextElement(table, 0xff33ccff,  fontSize, "Multicolored ", font, display, 0);

        //And of course, the mysterious centered text.
        CenterContainer center = new CenterContainer(margin);
        new TextElement(center, 0xff3366ff, fontSize, "Mysteriously Centered Text", font, display, 0);

        render.OnRender += (delta) => {frame(delta, render, display, plane);};
        render.Run();
    }

    private static void frame(double delta, IRender render, IDisplay display, BasicGUIPlane plane)
    {
        //the RenderDisplay talks to the Render for us.
        Vector2 size = render.WindowSize();
        plane.SetSize((int)size.X, (int)size.Y);
        plane.Iterate();
        plane.Draw();
    }
}