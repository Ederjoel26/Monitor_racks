using Monitor_racks.Models;

namespace Monitor_racks.Views;
[QueryProperty(nameof(ID), "ID")]
[QueryProperty(nameof(IDR), "IDR")]
public partial class Rack : ContentPage
{
	public string ID { get; set; }
	public string IDR { get; set; }

    private double fWidth = 0;
    private double fHeight = 0;

    private List<Label> lstlblRack;
    private List<Image> lstimgRack;

    public Rack()
	{
		InitializeComponent();

        fWidth = Width;
        fHeight = Height;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Title = $"Site {ID} Rack {IDR}";
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (fWidth != width || fHeight != height)
        {
            fWidth = width;
            fHeight = height;

            ActualizarVista();

            lstlblRack = (fWidth > fHeight)
                ? new List<Label> { lblRackHumedadH, lblRackTemperaturaH, lblRackPuertasH, lblRackLuzH, lblRackElectricidadH }
                : new List<Label> { lblRackHumedadV, lblRackTemperaturaV, lblRackPuertasV, lblRackLuzV, lblRackElectricidadV };

            lstimgRack = (fWidth > fHeight)
                ? new List<Image> { imgRackHumedadH, imgRackTemperaturaH, imgRackPuertasH, imgRackLuzH, imgRackElectricidadH }
                : new List<Image> { imgRackHumedadV, imgRackTemperaturaV, imgRackPuertasV, imgRackLuzV, imgRackElectricidadV };

            clsRack.EmpezarEscuchar($"Site{ID}", $"Rack{IDR}", lstlblRack, lstimgRack, true);
        }
    }

    private async void ActualizarVista()
    {
        if (fWidth > fHeight)
        {
            cvHorizontal.IsVisible = true;
            cvVertical.IsVisible = false;
        }
        else
        {
            cvHorizontal.IsVisible = false;
            cvVertical.IsVisible = true;
        }

    }
}