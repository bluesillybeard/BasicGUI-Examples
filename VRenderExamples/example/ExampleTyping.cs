namespace Examples;

using BasicGUI;

using VRender;

using OpenTK.Mathematics;
public class ExampleTyping
{
    public static void Run()
    {
        RenderSettings settings = new RenderSettings(){
            Size = new Vector2i(800, 600),
            VSync = true,
            BackgroundColor = 0x000000FF,
            TargetFrameTime = 1/60f,
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

        BasicGUIPlane plane = new BasicGUIPlane(800, 600, display);
        CenterContainer center = new CenterContainer(plane.GetRoot());
        StackingContainer container = new StackingContainer(center, StackDirection.right, 20);
        TextBoxElement textBox1 = new TextBoxElement(container, 20, 0xffffffff, font, display, 0);
        textBox1.back = new ColorOutlineRectElement(textBox1, 0x005555FF, 20, 20, 20, 20, 2, 0);
        TextBoxElement textBox2 = new TextBoxElement(container, 20, 0xffffffff, font, display, 0);
        textBox2.back = new ColorOutlineRectElement(textBox2, 0x664400FF, 20, 20, 20, 20, 2, 0);
        //main loop.
        render.OnRender += (delta) => {frame(delta, render, display, plane);};
        render.OnUpdate += (delta) => {Update(delta, render, display, plane);};
        render.Run();
    }

    private static void frame(double delta, IRender render, IDisplay display, BasicGUIPlane plane)
    {
        //draw the stuff
        plane.Draw();
    }
    private static void Update(double delta, IRender render, IDisplay display, BasicGUIPlane plane)
    {
        //Update things.
        Vector2 size = render.WindowSize();
        plane.SetSize((int)size.X, (int)size.Y);
        plane.Iterate();
    }
}