using ViSa.Interpretation;
using ViSa.Melody;
using ViSa.Structural;
using VsConsole;

namespace ConsViSa.Menus.SubMenus;
public class MenuCompose : AMenu
{
    private readonly IMesure _mesure;
    private readonly INoteComposer _compositor;

    public MenuCompose(
            IMesure mes,
            INoteComposer compositor
        )
        : base("Compose")
    {
        _mesure = mes;
        _compositor = compositor;
    }

    protected override void SetupCommands()
    {
        AddCommand("Random 4Bar", Bar0);
    }

    private void Bar0()
    {
        IMesure harmony;

        harmony = _compositor.GetMesure();

        DisplayBar(harmony);
    }
    private void DisplayBar(IMesureMelody mesure)
    {
        string message = string.Empty;

        foreach (INote note in mesure.Notes)
        {
            message += $"{note.ToString()} ";
        }

        AddFooterMessage(message);
    }
}
